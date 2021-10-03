using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixController : MonoBehaviour
{
    public Mix mix;

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
        mix.totalAmount += chemical.amount;
        UIController.Instance.bar.SetStability(mix.GetStability());
    }
}
