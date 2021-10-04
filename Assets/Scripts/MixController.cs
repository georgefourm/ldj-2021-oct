using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixController : MonoBehaviour
{
    public Mix mix;

    public float PourRate = 0.05f;

    private GameConfig config;

    private Color current = Color.black;

    void Start()
    {
        mix = new Mix();
        config = GameController.Instance.GetComponent<GameConfig>();
    }

    public void AddChemical(ChemicalComponent chemical)
    {
        if (current == Color.black) { current = chemical.chemicalColor.color; return; }

        var reaction = config.getReaction(
            current,
            chemical.chemicalColor.color
        );

        foreach (var rule in reaction.rules)
        {

            Debug.Log(rule);
            rule.apply(mix);
        }
        Debug.Log("---");
        mix.trueColor = current;
        mix.Amount = Mathf.Clamp01(mix.Amount + PourRate);
        UIController.Instance.bar.SetStability(mix.Amount);
        current = reaction.output;
    }
}
