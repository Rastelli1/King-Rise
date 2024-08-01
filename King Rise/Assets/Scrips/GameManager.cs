using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject textPanel;
    [SerializeField] Transform player;

    [SerializeField] float metaAlcanzar;

    [SerializeField] TMP_Text textScore;
    [SerializeField] TMP_Text dayFinal;

    private float positionInicio;
    private float meta;

    private int days = 0;

    private bool state=false;
    private bool isPaused=false;

    public static GameManager instance {  get; private set; }

    private void Start()
    {
        positionInicio= player.position.y;
        meta=positionInicio + metaAlcanzar;
        textScore.text = "DAY: " + days;
    }
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
        NextDay();
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
            textPanel.SetActive(false);
        }
        else
        {
            panel.SetActive(false);
            textPanel.SetActive(true);
        }
    }
    public void GameOver()
    {
        state = true;
        PausedState();
        ControllerScenes.instance.NewPlay(() =>
        {
            // Código que se ejecuta después de que NewPlay y TransitionGameOver terminan
            gameOverPanel.SetActive(true);
            
            textPanel.SetActive(false);
            dayFinal.text = "" + days;
        });
    }
    //public void Win()
    //{
    //    gameOverPanel.SetActive(true);
    //    state = true;
    //}

    public void NextDay()
    {
        if (player.position.y>= meta)
        {
            days = days + 1;
            textScore.text = "DAY: " + days;
            positionInicio = player.position.y;
            meta = positionInicio + metaAlcanzar;
        }
    }
}
