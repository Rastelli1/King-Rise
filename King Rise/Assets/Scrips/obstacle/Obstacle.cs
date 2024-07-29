using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] float despawnDistance;

    Camera mainCamera ; // tomo referencia de la camara principal

    private int rotationDirection; // 1 para derecha, -1 para izquierda

    private void Start()
    {
        mainCamera = Camera.main;
        rotationDirection = Random.Range(0, 2) == 0 ? -1 : 1;
    }
    void Update()
    {
        Rotation();
        Despawn();
    }

    private void Rotation()
    {
        float rotationAmount = rotationSpeed * Time.deltaTime * rotationDirection;

        transform.Rotate(Vector3.forward, rotationAmount);
    }

    private void Despawn()
    {
        if (transform.position.y < mainCamera.transform.position.y - despawnDistance) // cuando este el obstaculo a una distancia de 10 en y se eliminara
        {
            Destroy(gameObject);
        }
    }
}
