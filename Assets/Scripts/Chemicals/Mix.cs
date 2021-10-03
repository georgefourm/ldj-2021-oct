using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Mix
{
    public float Amount { get; set; }

    public float Color { get; private set; }

    public float Smoke { get; private set; }

    public float Wobble { get; private set; }


    public float GetStability()
    {
        return 0.5f * Wobble + 0.3f * Smoke + 0.2f * Color;
    }

    public float GetProperty(ChemicalProperty property)
    {
        switch (property)
        {
            case ChemicalProperty.Color:
                return Color;
            case ChemicalProperty.Smoke:
                return Smoke;
            case ChemicalProperty.Wobble:
                return Wobble;
        }
        throw new NotImplementedException("Not implemented property requested: " + property);
    }

    public void SetPropery(ChemicalProperty property, float propertyValue)
    {
        switch (property)
        {
            case ChemicalProperty.Color:
                Color = Mathf.Clamp01(propertyValue);
                break;
            case ChemicalProperty.Smoke:
                Smoke = Mathf.Clamp01(propertyValue);
                break;
            case ChemicalProperty.Wobble:
                Wobble = Mathf.Clamp01(propertyValue);
                break;
        }
    }
}
