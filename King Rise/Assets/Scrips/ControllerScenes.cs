using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class ControllerScenes : MonoBehaviour
{
    private Animator animator;
    private int aux = 0;
    private bool transitionGameOver;
    [SerializeField] private AnimationClip animacionFinal;

    public static ControllerScenes instance { get; private set; }
    void Start()
    {
        animator = GetComponent<Animator>();
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
    public void Intro()
    {
        StartCoroutine(IntroTransition());
    }
    public void Play()
    {
        animator.SetBool("GameOver", false);
        StartCoroutine(Transition());
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void Exit()
    {
        Application.Quit();
    }

    void OnDisable()
    {
        Destroy(gameObject);
    }

    IEnumerator Transition()
    {
        animator.SetBool("Play", true);
        yield return new WaitForSecondsRealtime(animacionFinal.length);
        SceneManager.LoadScene("Pruebas", LoadSceneMode.Single);
    }
    IEnumerator IntroTransition()
    {
        animator.SetBool("Play", true);
        yield return new WaitForSecondsRealtime(animacionFinal.length);
        SceneManager.LoadScene("Intro", LoadSceneMode.Single);
    }
    public void NewPlay(Action onComplete)
    {
        animator.SetBool("Play", true);
        StartCoroutine(TransitionGameOver(() => {
            // Llama al callback después de la transición
            onComplete?.Invoke();
        }));
    }
    private IEnumerator TransitionGameOver(Action onComplete)
    {
        // Lógica de transición
        yield return new WaitForSecondsRealtime(2); // Simulación de espera
        animator.SetBool("Play", false);
        animator.SetBool("GameOver", true);
        onComplete?.Invoke();
    }
}
