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

        if (mix.Amount == 0 || mix.Color.Equals(chemical.color))
        {
            mix.Color = chemical.color;
            mix.Amount += PourAmount;
            beaker.UpdateProperties();
            return;
        }

        var reaction = config.GetReaction(mix.Color, chemical.color);

        if (reactionTimer < ReactionSecs)
        {
            switch (reaction.Property)
            {
                case ChemicalProperty.Smoke:
                    mix.Smoke += (reaction.Increase ? 1 : -1) * reaction.Amount;
                    break;
                case ChemicalProperty.Wobble:
                    mix.Wobble += (reaction.Increase ? 1 : -1) * reaction.Amount;
                    break;
                default:
                    Debug.LogWarning("Invalid chemical property: " + reaction.Property);
                    break;
            }
            reactionTimer += Time.deltaTime;
        }
        else
        {
            mix.Color = reaction.Result;
            reactionTimer = 0;
        }


        mix.Amount += PourAmount;
        beaker.UpdateProperties();

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
