using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    public StabilityBarController bar;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        bar = GetComponentInChildren<StabilityBarController>();
    }
}
