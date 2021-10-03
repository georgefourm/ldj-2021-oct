using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WobbleConfig
{
    public float smoothness { get; }

    public float translocationX { get; }

    public float translocationY { get; }

    public WobbleConfig(float smoothness, float translocX, float translocY)
    {
        this.smoothness = smoothness;
        translocationX = translocX;
        translocationY = translocY;
    }

    public WobbleConfig(float smoothness, float translocation) : this(smoothness, translocation, translocation) { }
}
