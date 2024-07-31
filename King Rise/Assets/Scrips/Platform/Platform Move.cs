using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UIElements;

public class PlatformMove : MonoBehaviour
{
    [SerializeField] GameObject objetMove;
    [SerializeField] float velocity;
    [SerializeField] private GameObject ara�a;

    [Header("Animaciones")]
    private Animator animator;
    private Rigidbody2D rb2D; // Referencia al Rigidbody2D del objeto padre

    private Vector3 start;
    private Vector3 end;
    private Vector3 moveDirection;

    private bool flip = false;
    private bool isFalling = false; // Indica si la plataforma est� cayendo
    private bool isFlipped = false; // Indica si la plataforma ya se dio vuelta

    private bool canInteract = true; // Indica si la plataforma est� disponible para interacci�n

    void Start()
    {
        rb2D = GetComponentInParent<Rigidbody2D>(); // Obt�n el Rigidbody2D del objeto padre

        // Obtener el Animator del hijo
        animator = GetComponentInChildren<Animator>();

        if (animator == null)
        {
            Debug.LogWarning("Animator component is missing on the child game object.");
        }

        float yPosition = transform.position.y;

        // Establecer las posiciones de los puntos start y end
        start = new Vector3(3, yPosition, transform.position.z);
        end = new Vector3(-3, yPosition, transform.position.z);
        moveDirection = end;

        if (rb2D != null)
        {
            rb2D.bodyType = RigidbodyType2D.Kinematic; // Establece el Rigidbody2D como cinem�tico por defecto
        }
    }

    void Update()
    {
        if (!isFalling)
        {
            objetMove.transform.position = Vector3.MoveTowards(objetMove.transform.position, moveDirection, velocity * Time.deltaTime);

            if (objetMove.transform.position == end)
            {
                moveDirection = start;
                Flip();
            }
            else if (objetMove.transform.position == start)
            {
                moveDirection = end;
                Flip();
            }
        }
    }

    private void Flip()
    {
        if (ara�a != null)
        {
            flip = !flip;
            Vector3 escala = ara�a.transform.localScale;
            escala.x *= -1;
            ara�a.transform.localScale = escala;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Move playerMovement = collision.gameObject.GetComponent<Move>();
            if (playerMovement != null && !playerMovement.isJumping && canInteract)
            {
                if (animator != null)
                {
                    animator.SetBool("Dead", true);
                }
                StartFalling();
            }
        }
    }

    private void StartFalling()
    {
        if (!isFlipped)
        {
            // Gira el objeto verticalmente
            if (ara�a != null)
            {
                Vector3 rotation = ara�a.transform.rotation.eulerAngles;
                rotation.z = 270; // Rotar 180 grados alrededor del eje X
                ara�a.transform.rotation = Quaternion.Euler(rotation);
            }

            isFlipped = true; // Marca que la plataforma ya se dio vuelta
        }

        isFalling = true; // Marca que la plataforma debe caer
        canInteract = false; // Desactiva la interacci�n para evitar m�ltiples toques

        if (rb2D != null)
        {
            rb2D.bodyType = RigidbodyType2D.Dynamic; // Cambia el Rigidbody2D a din�mico
        }
    }

    // M�todo para habilitar la interacci�n si es necesario
    public void EnableInteraction()
    {
        canInteract = true; // Vuelve a permitir la interacci�n
    }
}
