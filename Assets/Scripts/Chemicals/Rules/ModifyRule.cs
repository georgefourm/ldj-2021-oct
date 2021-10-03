using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModificationRule", menuName = "Chemicals/Modification Rule", order = 1)]
public class ModifyRule : Rule
{
    public ChemicalProperty AffectedProperty;

    [Range(0, 1)]
    public float Amount;

    public bool Remove = false;

    public override void apply(Mix mix)
    {
        if (Remove)
        {
            mix.SetPropery(AffectedProperty, mix.GetProperty(AffectedProperty) - Amount);
        }
        else
        {
            mix.SetPropery(AffectedProperty, mix.GetProperty(AffectedProperty) + Amount);
        }
    }

    public override string ToString()
    {
        return string.Format("Amount: {0} Remove: {1} Property: {2}", Amount, Remove, AffectedProperty);
    }
}
