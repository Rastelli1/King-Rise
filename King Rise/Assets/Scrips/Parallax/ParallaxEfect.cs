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
        startPosition = transform.position.y; // Posici�n inicial de la capa
    }

    void LateUpdate()
    {
        float deltaY = (mainCameraTransform.position.y - previousCameraPosition.y) * multiplier; // Cantidad de movimiento de la c�mara en la coordenada Y desde el frame anterior
        float moveAmount = mainCameraTransform.position.y * (1 - multiplier); // Cu�nto se ha movido la c�mara con respecto a la capa
        transform.Translate(new Vector3(0, deltaY, 0)); // Mover la capa seg�n el movimiento de la c�mara
        previousCameraPosition = mainCameraTransform.position; // Actualizar la posici�n anterior de la c�mara

        // Reposicionamiento de capas hacia adelante
        if (moveAmount > startPosition + spriteHeight) // Si la c�mara est� m�s adelante que la capa, habr� reposicionamiento de capas
        {
            transform.Translate(new Vector3(0, spriteHeight, 0));
            startPosition += spriteHeight;
        }

        // Reposicionamiento de capas hacia atr�s
        if (moveAmount < startPosition )
        {
            transform.Translate(new Vector3(0, -spriteHeight, 0));
            startPosition -= spriteHeight;
        }
    }
}
