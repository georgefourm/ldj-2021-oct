using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeakerController : MonoBehaviour
{
    public ParticleSystem smoke;

    public Transform Fluid;

    public Gradient colorScale;

    public float GrowSpeed = 8;

    public float MaxAmount = 19f;

    public bool GameRunning = true;

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

    void SetTrueColor(Color color)
    {
        var main = smoke.main;
        main.startColor = color;
        mesh.material.color = color;
    }

    void SetColor(float color)
    {
        var finalColor = colorScale.Evaluate(color);
        var main = smoke.main;

        main.startColor = finalColor;
        mesh.material.color = finalColor; 
    }

    void SetWobble(float wobble)
    {
        var wobbleLvl = WobbleLevel.EXTREME;
        if (wobble < 0.2) wobbleLvl = WobbleLevel.NO;
        else if (wobble < .4) wobbleLvl = WobbleLevel.LOW;
        else if (wobble < .6) wobbleLvl = WobbleLevel.MED;
        else if (wobble < .8) wobbleLvl = WobbleLevel.HIGH;
        wobbleController.SetWobbleLevel(wobbleLvl);
    }

    public void Clear()
    {
        mixController.mix = new Mix();
        shatterController.Unshatter();
        Fluid.localScale += Vector3.forward * (0.1f - Fluid.localScale.z);
        targetLevel = 0.1f;
        UpdateProperties();
    }

    private void SetAmount(float amount)
    {
        targetLevel = amount * MaxAmount;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Chemical" || !GameRunning)
        {
            return;
        }
        var bottle = other.GetComponentInParent<BottleController>();
        if (bottle.IsPouring())
        {
            mixController.AddChemical(bottle.chemical);
            UpdateProperties();
            GameController.Instance.CheckWin(mixController.mix);
            bottle.SetDepleted();
        }
    }

    private void UpdateProperties()
    {
        SetAmount(mixController.mix.Amount);
        SetSmokiness(mixController.mix.Smoke);
        SetTrueColor(mixController.mix.trueColor);
        SetWobble(mixController.mix.Wobble);
    }

    public void Shatter()
    {
        shatterController.Shatter(false);
    }
}
