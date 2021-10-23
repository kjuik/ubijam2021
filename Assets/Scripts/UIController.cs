using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public CanvasGroup startPanel;
    public CanvasGroup losePanel;
    public CanvasGroup winPanel;

    public float alphaSpeed;

    List<CanvasGroup> allPanels;

    void Awake()
    {
        startPanel.alpha = 1f; 
        losePanel.alpha = 0f;
        winPanel.alpha = 0f;

        allPanels = new List<CanvasGroup>
        {
            startPanel, losePanel, winPanel
        };
    }

    void Update()
    {
        CanvasGroup currentPanel = null;
        switch (GameManager.Instance.CurrentState)
        {
            case GameManager.State.Started:
                currentPanel = startPanel;
                break;
            case GameManager.State.Playing:
                currentPanel = null;
                break;
            case GameManager.State.Lost:
                currentPanel = losePanel;
                break;
            case GameManager.State.Won:
                currentPanel = winPanel;
                break;
        }

        foreach(var panel in allPanels)
        {
            if (panel == currentPanel && currentPanel.alpha < 1f)
            {
                panel.alpha += alphaSpeed * Time.deltaTime;
            }
            else if (panel.alpha > 0f)
            {
                panel.alpha -= alphaSpeed * Time.deltaTime;
            }
        }
    }
}
