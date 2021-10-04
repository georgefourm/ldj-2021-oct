using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalController : MonoBehaviour
{
    public Chemical Chemical;

    public MeshRenderer Fluid;

    public ParticleSystem Particles;

    private MixController mixController;

    public float fluidTimeLeft = 3.0f;

    private float pourAfter;

    public float fluidPourRate = 0.5f;

    private bool pouring = false;

    public void Initialize(Material material)
    {
        mixController = FindObjectOfType<MixController>();
        Fluid.material = material;

        var main = Particles.main;
        main.startColor = material.color;
        Chemical = new Chemical(material.color);

        pourAfter = fluidTimeLeft;
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
            if (isTilted() && fluidTimeLeft > 0)
            {
                fluidTimeLeft -= Time.deltaTime;
                if (fluidTimeLeft < pourAfter)
                {
                    pourAfter -= fluidPourRate;
                    mixController.AddChemical(Chemical, fluidPourRate / 50);
                }
            }
            if (fluidTimeLeft <= 0)
            {
                Fluid.gameObject.SetActive(false);
            }
        }
    }

    private void FixedUpdate()
    {
        if (isTilted() && fluidTimeLeft > 0)
        {
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
