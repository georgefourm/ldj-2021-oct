using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixController : MonoBehaviour
{
    public Mix mix;

    private GameConfig config;

    private BeakerController beaker;

    public float changeColorTimer = 3.0f;

    private float changeColorTimeLeft;

    void Start()
    {
        mix = new Mix();
        config = GameController.Instance.GetComponent<GameConfig>();
        beaker = GetComponent<BeakerController>();
        changeColorTimeLeft = changeColorTimer;
    }

    public void AddChemical(Chemical chemical, float fluidPourRate)
    {
        if (mix.Amount == 0)
        {
            mix.Color = chemical.color;
        }

        if (mix.Color != chemical.color)
        {
            var reaction = config.GetReaction(mix.Color, chemical.color);

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

            changeColorTimeLeft -= fluidPourRate;
            if (changeColorTimeLeft < 0)
            {
                mix.Color = reaction.Result;
            }
        }

        mix.Amount += fluidPourRate;
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
