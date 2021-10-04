using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameController : MonoBehaviour
{
    [Range(0, 1)]
    public float TargetStability = 0.7f;

    [Range(0, 1)]
    public float ErrorThreshold = 0.1f;

    public UIController ui;

    public static GameController Instance;

    public BeakerController Beaker;

    private ChemicalSpawner[] Spawners;

    public bool GameRunning { get; private set; }

    public float Score { get; private set; }

    [Range(0,0.5f)]
    public float ScoreStep = 0.1f;

    private void Start()
    {
        Spawners = FindObjectsOfType<ChemicalSpawner>();

        GameRunning = true;
        Color[] colors = Spawners.Select(spawner => spawner.material.color).ToArray();
        GetComponent<GameConfig>().GenerateConfig(colors);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void IncreaseScore()
    {
        Score = Mathf.Clamp01(Score+ScoreStep);
        ui.bar.SetStability(Score);
        if (Score >= 1)
        {
            Win();
        }
    }

    public void Win()
    {
        GameRunning = false;
        ui.ActivateGameOver("Success", "Component Stabilized");
    }

    public void Lose()
    {
        Beaker.Shatter();
        GameRunning = false;
        ui.ActivateGameOver("Failure", "Component Exploded");
    }

    public void Restart()
    {
        Score = 0;
        ui.bar.SetStability(0f);
        ui.DeactivateGameOver();

        ResetBottles();
        Beaker.Clear();

        GameRunning = true;
    }

    public void ResetBottles()
    {
        foreach (var spawner in Spawners)
        {
            spawner.Respawn();
        }
    }
}
