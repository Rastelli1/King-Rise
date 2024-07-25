using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerScenes : MonoBehaviour
{
    public void Play()
    {
        Debug.Log("Loading Pruebas scene...");
        SceneManager.LoadScene("Pruebas", LoadSceneMode.Single);
    }

    public void Menu()
    {
        Debug.Log("Loading Menu scene...");
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void Exit()
    {
        Debug.Log("Exiting application...");
        Application.Quit();
    }

    void OnDisable()
    {
        Debug.Log("ControllerScenes disabled, destroying gameObject...");
        Destroy(gameObject);
    }
}
