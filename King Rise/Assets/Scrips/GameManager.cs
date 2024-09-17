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
    [SerializeField] GameObject panelFinalPisos;
    private Transform player;  // Ya no es serializado, se asigna din�micamente
    GameObject target;

    [SerializeField] float metaAlcanzar;

    [SerializeField] TMP_Text dayFinal;

    [Header("Sonido")]
    [SerializeField] private AudioSource backgroundMusicSource;
    [SerializeField] private AudioSource deathMusicSource;
    [SerializeField] private AudioClip levelMusic;
    [SerializeField] private AudioClip deathMusic;

    [Header("Sistema de pisos")]
    [SerializeField] TMP_Text pisos;
    [SerializeField] TMP_Text pisoFinal;
    [SerializeField] private float intervaloDePisos = 10f;
    private float posicionPlayer;
    private float intervaloPisos = 10f;
    private float pisoAllegar;
    private int pisoActual = 0;

    private int days = 0;

    private bool state = false;
    private bool isPaused = false;

    public static GameManager instance { get; private set; }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        if (target != null)
        {
            player = target.transform;  // Asigna el Transform del objeto encontrado
        }
        else
        {
            Debug.LogWarning("No se encontr� ning�n objeto con la etiqueta 'Player'.");
        }
        panelFinalPisos.SetActive(false);
        posicionPlayer = player.position.y;
        pisoActual = 0;
        pisoAllegar = posicionPlayer + intervaloPisos;
        pisos.text = "" + pisoActual;
        StopDeathMusic();
        PlayLevelMusic();
        StartCoroutine(DayTimer()); // Inicia la corrutina para avanzar d�as
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
        Aumentarpiso();
    }

    public void PlayLevelMusic()
    {
        if (backgroundMusicSource != null && levelMusic != null)
        {
            backgroundMusicSource.clip = levelMusic;
            backgroundMusicSource.loop = true;
            backgroundMusicSource.Play();
        }
    }

    public void PlayDeathMusic()
    {
        if (deathMusicSource != null && deathMusic != null)
        {
            deathMusicSource.clip = deathMusic;
            deathMusicSource.loop = false;
            deathMusicSource.Play();
        }
    }

    public void StopLevelMusic()
    {
        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.Stop();
        }
    }

    public void StopDeathMusic()
    {
        if (deathMusicSource != null)
        {
            deathMusicSource.Stop();
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
        StopLevelMusic();
        PausedState();
        ControllerScenes.instance.NewPlay(() =>
        {
            textPanel.SetActive(false);
            gameOverPanel.SetActive(true);
            panelFinalPisos.SetActive(true);
            PlayDeathMusic();
            dayFinal.text = "" + days;
            pisoFinal.text = "" + pisoActual;
        });
    }

    private IEnumerator DayTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(metaAlcanzar); // Espera la cantidad de tiempo definida
            NextDay(); // Avanza al siguiente d�a
        }
    }

    public void NextDay()
    {
        days += 1;
    }

    private void Aumentarpiso()
    {
        if (player.position.y >= pisoAllegar)
        {
            posicionPlayer = player.position.y;
            pisoAllegar = posicionPlayer + intervaloPisos;
            pisoActual += 5;
            pisos.text = "" + pisoActual;
        }
    }
}
