using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerScenes : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Pruebas", LoadSceneMode.Single);
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
}
