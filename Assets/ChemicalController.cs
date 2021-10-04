using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalController : MonoBehaviour
{
    public Chemical Chemical;

    public MeshRenderer Fluid;

    public ParticleSystem Particles;

    private MixController mixController;

    public float fluidAmountLeft = 0.3f;

    public float fluidPourRate = 0.05f;

    private bool pouring = false;

    public void Initialize(Material material)
    {
        mixController = FindObjectOfType<MixController>();
        Fluid.material = material;

        var main = Particles.main;
        main.startColor = material.color;
        Chemical = new Chemical(material.color);
    }

    private bool isTilted()
    {
        var parentRotation = transform.parent.rotation;
        return parentRotation.eulerAngles.z > 90 && parentRotation.eulerAngles.z < 270;
    }

    private void Update()
    {
        if (pouring)
        {
            if (isTilted() && fluidAmountLeft > 0)
            {
                mixController.AddChemical(Chemical, fluidPourRate);
            }
            if (fluidAmountLeft <= 0)
            {
                Fluid.gameObject.SetActive(false);
            }
        }
    }

    private void FixedUpdate()
    {
        if (isTilted() && fluidAmountLeft > 0)
        {
            fluidAmountLeft -= fluidPourRate;
            if (!Particles.gameObject.activeSelf)
            {
                Particles.gameObject.SetActive(true);
                Particles.Play();
            }
        }
        else
        {
            Particles.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Beaker" || !GameController.Instance.GameRunning)
        {
            return;
        }
        pouring = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Beaker") pouring = false;
    }
}
