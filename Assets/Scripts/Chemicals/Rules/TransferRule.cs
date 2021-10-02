using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TransferRule", menuName = "Chemicals/Transfer Rule", order = 1)]
public class TransferRule : Rule
{
    [Range(0,1)]
    public float TransferPercentage;

    public ChemicalProperty SourceProperty, TargetProperty;

    public override void apply(Mix mix)
    {
        float currValue = mix.RetrieveProperty(SourceProperty);
        float percentage = currValue * (TransferPercentage / 100);

        mix.ChangeProperty(SourceProperty,Mathf.Max(0,currValue-percentage));
        mix.ChangeProperty(TargetProperty, Mathf.Min(1, currValue + percentage));
    }
}
