using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Mix
{
    private float amount, smoke, wobble;

    public float Amount { get { return amount; } set { amount = Mathf.Clamp01(value); } }

    public Color Color { get; set; }

    public float Smoke { get { return smoke; } set { smoke = Mathf.Clamp01(value); } }

    public float Wobble { get { return wobble; } set { wobble = Mathf.Clamp01(value); } }

    public bool IsStable()
    {
        return Wobble < 1 && Smoke < 1 && Amount < 1;
    }

}
