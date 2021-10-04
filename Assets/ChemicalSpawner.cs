using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalSpawner : MonoBehaviour
{
    public GameObject ChemicalPrefab;

    public Material material;

    private GameObject instance;

    private void Start()
    {
        Spawn();
    }

    private void Update()
    {
        if (instance == null)
        {
            Spawn();
        }
    }

    public void Respawn()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        Spawn();
    }

    private void Spawn()
    {
        instance = Instantiate(ChemicalPrefab);
        instance.transform.position = transform.position;
        var controller = instance.GetComponentInChildren<ChemicalController>();
        controller.Initialize(material);
    }

}
