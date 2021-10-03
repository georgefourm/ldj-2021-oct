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

    private MeshRenderer mesh;

    private MixController mixController;

    private float targetLevel = 0.1f;

    void Start()
    {
        mesh = Fluid.GetComponent<MeshRenderer>();
        mixController = GetComponent<MixController>();
    }

    private void Update()
    {
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

    void SetColor(float color)
    {
        var finalColor = colorScale.Evaluate(color);
        var main = smoke.main;

        main.startColor = finalColor;
        mesh.material.color = finalColor; 
    }

    public void Clear()
    {
        mixController.mix = new Mix();
        Fluid.localScale += Vector3.forward * (0.1f - Fluid.localScale.z);
    }

    private void SetAmount(float amount)
    {
        targetLevel = amount * MaxAmount;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Chemical")
        {
            return;
        }
        var bottle = other.GetComponentInParent<BottleController>();
        if (bottle.IsPouring())
        {
            mixController.AddChemical(bottle.chemical);
            UpdateProperties();
            GameController.Instance.CheckWin(mixController.mix);
        }
    }

    private void UpdateProperties()
    {
        SetAmount(mixController.mix.Amount);
        SetSmokiness(mixController.mix.Smoke);
        SetColor(mixController.mix.Color);
    }
}
