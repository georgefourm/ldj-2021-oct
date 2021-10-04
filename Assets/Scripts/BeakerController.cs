using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeakerController : MonoBehaviour
{
    public ParticleSystem smoke;

    public Transform Fluid;

    public float GrowSpeed = 8;

    public float MaxAmount = 19f;

    private MeshRenderer mesh;

    private MixController mixController;

    private float targetLevel = 0.1f;

    private WobbleController wobbleController;

    private ShatterController shatterController;

    void Start()
    {
        mesh = Fluid.GetComponent<MeshRenderer>();
        mixController = GetComponent<MixController>();
        wobbleController = GetComponentInChildren<WobbleController>();
        shatterController = GetComponentInChildren<ShatterController>();
        UpdateProperties();
    }

    private void Update()
    {
        if (Fluid == null) return;
        var currentScale = Fluid.localScale.z;

        if (targetLevel != currentScale)
        {
            int dir = targetLevel > currentScale ? 1 : -1;
            currentScale += dir * GrowSpeed * Time.deltaTime;
            var scale = Mathf.Clamp(currentScale, 0, dir == 1 ? targetLevel : currentScale);
            Fluid.localScale += Vector3.forward * (scale - Fluid.localScale.z);
        }
    }

    void SetSmokiness(float level)
    {
        level = Mathf.Clamp(level, 0, 1);
        var emission = smoke.emission;
        emission.rateOverTime = level * 5;
    }

    void SetColor(Color color)
    {
        var main = smoke.main;
        main.startColor = color;

        mesh.material.color = color; 
    }

    void SetWobble(float wobble)
    {
        var wobbleLvl = WobbleLevel.EXTREME;
        if (wobble < 0.1) wobbleLvl = WobbleLevel.NO;
        else if (wobble < .3) wobbleLvl = WobbleLevel.LOW;
        else if (wobble < .6) wobbleLvl = WobbleLevel.MED;
        else if (wobble < .8) wobbleLvl = WobbleLevel.HIGH;
        wobbleController.SetWobbleLevel(wobbleLvl);
    }

    public void Clear()
    {
        mixController.mix = new Mix();
        shatterController.Unshatter();
        ResetFluid();
        UpdateProperties();
    }

    private void SetAmount(float amount)
    {
        targetLevel = amount * MaxAmount;
    }

    public void UpdateProperties()
    {
        SetAmount(mixController.mix.Amount);
        SetSmokiness(mixController.mix.Smoke);
        SetWobble(mixController.mix.Wobble);
        SetColor(mixController.mix.Color);
    }

    public void Shatter()
    {
        ResetFluid();
        shatterController.Shatter(false);
    }

    private void ResetFluid()
    {
        Fluid.localScale += Vector3.forward * (0.1f - Fluid.localScale.z);
        targetLevel = 0.1f;
    }
}
