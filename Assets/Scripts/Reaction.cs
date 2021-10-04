using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaction
{
    public Color Result;

    public ChemicalProperty Property;

    public float Amount;

    public bool Increase = true;

    public Reaction(ChemicalProperty property, float amount, bool increase)
    {
        Property = property;
        Amount = amount;
        Increase = increase;
    }

    public Reaction(Reaction other, Color Result)
    {
        this.Result = Result;
        Property = other.Property;
        Amount = other.Amount;
        Increase = other.Increase;
    }

    public override string ToString()
    {
        return string.Format("{0} {1} by {2}",Increase? "Increase":"Decrease",Property.ToString(),Amount);
    }
}
