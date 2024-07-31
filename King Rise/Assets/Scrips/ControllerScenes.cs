using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerScenes : MonoBehaviour
{
    private Animator animator;


    [SerializeField] private AnimationClip animacionFinal;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Play()
    {

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
        animator.SetTrigger("Iniciar");
        yield return new WaitForSeconds(animacionFinal.length);
        SceneManager.LoadScene("Pruebas", LoadSceneMode.Single);
    }
    public void NewPlay()
    {

        SceneManager.LoadScene("Pruebas", LoadSceneMode.Single);
    }
}
