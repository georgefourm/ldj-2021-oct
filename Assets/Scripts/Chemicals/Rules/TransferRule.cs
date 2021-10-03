using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TransferRule", menuName = "Chemicals/Transfer Rule", order = 1)]
public class TransferRule : Rule
{
    [Range(0,1)]
    public float TransferAmount;

    public ChemicalProperty SourceProperty, TargetProperty;

    public override void apply(Mix mix)
    {
        var sourceValue = mix.GetProperty(SourceProperty);
        var transferredAmount = Mathf.Min(sourceValue,TransferAmount);

        mix.SetPropery(SourceProperty,sourceValue - transferredAmount);
        mix.SetPropery(TargetProperty,mix.GetProperty(TargetProperty) + transferredAmount);
    }
}
