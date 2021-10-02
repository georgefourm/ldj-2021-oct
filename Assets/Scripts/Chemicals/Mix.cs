using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mix
{
    public float totalAmount;

    public float color;

    public float smoke;

    public float wobble;

    public float RetrieveProperty(ChemicalProperty property)
    {
        switch (property)
        {
            case ChemicalProperty.Color:
                return color;
            case ChemicalProperty.Smoke:
                return smoke;
            case ChemicalProperty.Wobble:
                return wobble;
        }
        throw new NotImplementedException("Not implemented property requested: " + property);
    }

    public void ChangeProperty(ChemicalProperty property, float propertyValue)
    {
        switch (property)
        {
            case ChemicalProperty.Color:
                color = propertyValue;
                break;
            case ChemicalProperty.Smoke:
                smoke = propertyValue;
                break;
            case ChemicalProperty.Wobble:
                wobble = propertyValue;
                break;
        }
    }
}
