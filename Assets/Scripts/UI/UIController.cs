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

    public void ActivateGameOver(bool won, string messageText)
    {
        if (won)
        {
            mainText.text = "Success!";
        } else
        {
            mainText.text = "Whoops!";
        }
        
        message.text = messageText;
        GameOverPanel.SetActive(true);
    }

    public void DeactivateGameOver()
    {
        GameOverPanel.SetActive(false);
    }
}
