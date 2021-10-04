using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixController : MonoBehaviour
{
    public Mix mix;

    public float PourAmount = 0.05f;

    private GameConfig config;

    private BeakerController beaker;

    void Start()
    {
        mix = new Mix();
        config = GameController.Instance.GetComponent<GameConfig>();
        beaker = GetComponent<BeakerController>();
    }

    public void AddChemical(Chemical chemical)
    {
        if (mix.Amount == 0 || chemical.color.Equals(mix.Color))
        {
            mix.Color = chemical.color;
        }
        else
        {
            var reaction = config.GetReaction(mix.Color, chemical.color);
            Debug.Log("Performing reaction: " + reaction);
            mix.Color = reaction.Result;
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
        }

        mix.Amount += PourAmount;
        beaker.UpdateProperties();
    }
}
