using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    public StabilityBarController bar;

    public GameObject GameOverPanel;

    public Text mainText, message;

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
        bar.SetStability(0.0f);
    }

    public void ActivateGameOver(string text,string messageText)
    {
        mainText.text = text;
        message.text = messageText;
        GameOverPanel.SetActive(true);
    }

    public void DeactivateGameOver()
    {
        GameOverPanel.SetActive(false);
    }
}
