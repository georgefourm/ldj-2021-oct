using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModificationRule", menuName = "Chemicals/Modification Rule", order = 1)]
public class ModifyRule : Rule
{
    public ChemicalProperty AffectedProperty;

    [Range(0, 1)]
    public float ModifyPercentage;

    public bool Remove = false;

    public override void apply(Mix mix)
    {
        float currValue = mix.RetrieveProperty(AffectedProperty);
        float percentage = currValue * (ModifyPercentage / 100);
        if (Remove)
        {
            mix.ChangeProperty(AffectedProperty, currValue - percentage);
        }
        else
        {
            mix.ChangeProperty(AffectedProperty, currValue + percentage);
        }
    }
}
