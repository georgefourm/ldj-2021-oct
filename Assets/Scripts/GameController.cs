using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float TargetStability = 0.7f;

    public float ErrorThreshold = 0.1f;

    public UIController ui;

    public static GameController Instance;

    public ChemicalSpawner[] Spawners;

    public BeakerController Beaker;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void CheckWin(Mix mix)
    {
        var stability = mix.GetStability();
        if (stability >= TargetStability && stability <= TargetStability + ErrorThreshold)
        {
            Win();
        }
        if (stability > TargetStability + ErrorThreshold)
        {
            Lose();
        }
        if (mix.Amount >= 1)
        {
            Lose();
        }
    }

    public void Win()
    {
        ui.ActivateGameOver("Success", "Component Stabilized");
    }

    public void Lose()
    {
        ui.ActivateGameOver("Failure", "Component Exploded");
    }

    public void Restart()
    {
        foreach (var spawner in Spawners)
        {
            spawner.Respawn();
        }
        Beaker.Clear();
        ui.DeactivateGameOver();
        ui.bar.SetStability(0f);
    }
}
