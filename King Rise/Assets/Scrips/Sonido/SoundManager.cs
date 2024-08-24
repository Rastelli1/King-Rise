using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    private void Awake()
    {
        // Verifica si ya existe una instancia de MusicManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        // Suscríbete al evento que se llama cuando cambia la escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Desuscríbete del evento cuando este objeto se desactive
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Verifica el nombre de la escena
        if (scene.name == "Pruebas")  // Cambia "Nivel" por el nombre de la escena de nivel
        {
            // Detén la música
            Destroy(gameObject);
        }
    }
}