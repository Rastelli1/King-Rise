using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Transition());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Transition()
    {
        yield return new WaitForSecondsRealtime(5);
        ControllerScenes.instance. Empezar();
    }
}
