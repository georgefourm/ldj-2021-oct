using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixController : MonoBehaviour
{
    public Mix mix;

    public float PourRate = 0.05f;

    void Start()
    {
        mix = new Mix();
    }

    public void AddChemical(ChemicalComponent chemical)
    {
        foreach (var rule in chemical.rules)
        {
            rule.apply(mix);
        }
        mix.Amount = Mathf.Clamp01(mix.Amount + PourRate);
        UIController.Instance.bar.SetStability(mix.GetStability());
    }
}
