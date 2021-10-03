using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeakerController : MonoBehaviour
{
    public ParticleSystem smoke;

    public Transform Fluid;

    private MeshRenderer mesh;

    private MixController mix;

    void Start()
    {
        mesh = Fluid.GetComponent<MeshRenderer>();
        mix = GetComponent<MixController>();
    }

    void SetSmokiness(float level)
    {
        level = Mathf.Clamp(level, 0, 1);
        var emission = smoke.emission;
        emission.rateOverTime = level * 5;
    }

    void SetLevel(float level)
    {
        level = Mathf.Clamp(level, 0, 1);
        Fluid.localScale += Vector3.forward * (level - Fluid.localScale.z);
    }

    void SetColor(Color color)
    {
        mesh.material.color = color;
    }

    private void SetScale(float scale)
    {
        Fluid.localScale += Vector3.forward * (scale - GetCurrentScale());
    }

    private float GetCurrentScale()
    {
        return Fluid.localScale.z;
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
            mix.AddChemical(bottle.chemical);
        }
    }
}
