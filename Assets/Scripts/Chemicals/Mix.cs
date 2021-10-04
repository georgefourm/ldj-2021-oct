using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Mix
{
    public float Amount { get; set; }

    public Color Color { get; set; }

    public float Smoke { get; set; }

    public float Wobble { get; set; }

    public bool IsStable()
    {
        return Wobble < 1 && Smoke < 1 && Amount < 1;
    }

}
