using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConditionalRule", menuName = "Chemicals/Conditional Rule", order = 1)]
public class ConditionalRule : Rule
{
    public ChemicalProperty ConditionProperty;

    [Range(0, 1)]
    public float ConditionPercentage;

    public bool ShouldExceed = false;

    public Rule TrueConditionRule,FalseConditionRule;

    public override void apply(Mix mix)
    {
        float currValue = mix.RetrieveProperty(ConditionProperty);
        bool isExceeding = currValue > ConditionPercentage;
        if (!(ShouldExceed ^ isExceeding))
        {
            TrueConditionRule.apply(mix);
        }
        else
        {
            FalseConditionRule.apply(mix);
        }
    }
}