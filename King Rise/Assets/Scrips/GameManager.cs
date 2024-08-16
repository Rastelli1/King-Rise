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
    private AudioSource audioSource;

    [SerializeField] float metaAlcanzar;

    [SerializeField] TMP_Text textScore;
    [SerializeField] TMP_Text dayFinal;


    private int days = 0;

    private bool state = false;
    private bool isPaused = false;

    public static GameManager instance { get; private set; }

    private void Start()
    {
        textScore.text = "DAY: " + days;
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        StartCoroutine(DayTimer()); // Start the coroutine to advance days every 20 seconds
    }

    private void Awake()
    {
        if (instance != null && instance != this)
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
        if (Input.GetKeyDown(KeyCode.Escape) && !state)
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

    private IEnumerator DayTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(metaAlcanzar); // Wait for 20 seconds
            NextDay(); // Advance to the next day
        }
    }

    public void NextDay()
    {
        days = days + 1;
        textScore.text = "DAY: " + days;
    }
}
