using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject gameOverPanel;

    private bool state=false;

    private bool isPaused=false;

    public static GameManager instance {  get; private set; }

    private void Awake()
    {
        if(instance != null && instance!= this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && state==false)
        {
            PausedState();
            PanelPaused();
        }
        
    }
    public void PausedState()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void PanelPaused()
    {
        if (isPaused)
        {
            panel.SetActive(true);
        }
        else
        {
            panel.SetActive(false);
        }
    }
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        PausedState();
        state = true;
    }
    //public void Win()
    //{
    //    gameOverPanel.SetActive(true);
    //    state = true;
    //}
}
