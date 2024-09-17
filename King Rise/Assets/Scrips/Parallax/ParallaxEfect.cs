using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectParralax : MonoBehaviour
{
    [SerializeField] private float multiplier;
    private Transform mainCameraTransform;
    private Vector3 previousCameraPosition;
    private float spriteHeight;
    private float startPosition;

    void Start()
    {
        mainCameraTransform = Camera.main.transform;
        previousCameraPosition = mainCameraTransform.position;
        spriteHeight = GetComponent<SpriteRenderer>().bounds.size.y; // Obtener la altura del sprite
        startPosition = transform.position.y; // Posición inicial de la capa
    }

    void LateUpdate()
    {
        float deltaY = (mainCameraTransform.position.y - previousCameraPosition.y) * multiplier; // Cantidad de movimiento de la cámara en la coordenada Y desde el frame anterior
        float moveAmount = mainCameraTransform.position.y * (1 - multiplier); // Cuánto se ha movido la cámara con respecto a la capa
        transform.Translate(new Vector3(0, deltaY, 0)); // Mover la capa según el movimiento de la cámara
        previousCameraPosition = mainCameraTransform.position; // Actualizar la posición anterior de la cámara

        // Reposicionamiento de capas hacia adelante
        if (moveAmount > startPosition + spriteHeight) // Si la cámara está más adelante que la capa, habrá reposicionamiento de capas
        {
            transform.Translate(new Vector3(0, spriteHeight, 0));
            startPosition += spriteHeight;
        }

        // Reposicionamiento de capas hacia atrás
        if (moveAmount < startPosition )
        {
            transform.Translate(new Vector3(0, -spriteHeight, 0));
            startPosition -= spriteHeight;
        }
    }
}
