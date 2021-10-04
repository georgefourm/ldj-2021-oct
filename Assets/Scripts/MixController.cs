using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixController : MonoBehaviour
{
    public Mix mix;

    public float PourAmount = 0.05f;

    public float PourInterval = 0.5f;

    public float ReactionSecs = 3;

    private GameConfig config;

    private BeakerController beaker;

    private Reaction currReaction;

    private float reactionTimer = 0f;

    private float pourTimer = 0f;

    void Start()
    {
        mix = new Mix();
        config = GameController.Instance.GetComponent<GameConfig>();
        beaker = GetComponent<BeakerController>();
    }

    public void Clear()
    {
        currReaction = null;
        mix = new Mix();
        reactionTimer = 0;
        pourTimer = 0;
    }

    public void AddChemical(Chemical chemical)
    {
        if (pourTimer < PourInterval)
        {
            pourTimer += Time.deltaTime;
            return;
        }
        else
        {
            pourTimer = 0f;
        }

        if (mix.Amount == 0)
        {
            mix.Color = chemical.color;
            mix.Amount += PourAmount;
            beaker.UpdateProperties();
            return;
        }

        if (!chemical.color.Equals(mix.Color))
        {
            var reaction = config.GetReaction(mix.Color, chemical.color);
            currReaction = reaction;
            mix.Color = reaction.Result;
            reactionTimer = 0f;
        }

        if (currReaction != null)
        {
            if (reactionTimer < ReactionSecs)
            {
                switch (currReaction.Property)
                {
                    case ChemicalProperty.Smoke:
                        mix.Smoke += (currReaction.Increase ? 1 : -1) * currReaction.Amount;
                        break;
                    case ChemicalProperty.Wobble:
                        mix.Wobble += (currReaction.Increase ? 1 : -1) * currReaction.Amount;
                        break;
                    default:
                        Debug.LogWarning("Invalid chemical property: " + currReaction.Property);
                        break;
                }
                reactionTimer += Time.deltaTime;
            }
            else
            {
                var reaction = config.GetReaction(mix.Color, chemical.color);
                currReaction = reaction;
                mix.Color = reaction.Result;
                reactionTimer = 0f;
            }

        }

        mix.Amount += PourAmount;
        beaker.UpdateProperties();

        if (currReaction != null)
        {
            if (mix.IsStable())
            {
                GameController.Instance.IncreaseScore();
            }
            else
            {
                GameController.Instance.Lose();
            }
        }
    }
}
