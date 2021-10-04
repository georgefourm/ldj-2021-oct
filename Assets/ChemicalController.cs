using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalController : MonoBehaviour
{
    public Chemical Chemical;

    public MeshRenderer Fluid;

    public ParticleSystem Particles;

    private MixController mixController;

    public bool IsDepleted { get; private set; }

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

    private void FixedUpdate()
    {
        if (isTilted() && !IsDepleted)
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
        if (isTilted() && !IsDepleted)
        {
            mixController.AddChemical(Chemical);
            Fluid.gameObject.SetActive(false);
            IsDepleted = true;
        }
    }
}
