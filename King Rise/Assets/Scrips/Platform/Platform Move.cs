using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UIElements;

public class PlatformMove : MonoBehaviour
{
    [SerializeField] GameObject objetMove;
    [SerializeField] float velocity;
    [SerializeField] private GameObject araña;
    private AudioSource audioSource;
    [SerializeField] AudioClip deathSound; // Asocia el clip de audio desde el Inspector
    [Header("Animaciones")]
    private Animator animator;
    private Rigidbody2D rb2D; // Referencia al Rigidbody2D del objeto padre

    private Vector3 start;
    private Vector3 end;
    private Vector3 moveDirection;

    private bool flip = false;
    private bool isFalling = false; // Indica si la plataforma está cayendo
    private bool isFlipped = false; // Indica si la plataforma ya se dio vuelta

    private bool canInteract = true; // Indica si la plataforma está disponible para interacción

    void Start()
    {
        rb2D = GetComponentInParent<Rigidbody2D>(); // Obtén el Rigidbody2D del objeto padre
        audioSource = GetComponent<AudioSource>();
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
            rb2D.bodyType = RigidbodyType2D.Kinematic; // Establece el Rigidbody2D como cinemático por defecto
        }
    }

    void Update()
    {
        IsFalling();
    }
    private void IsFalling()
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
        if (araña != null)
        {
            flip = !flip;
            Vector3 escala = araña.transform.localScale;
            escala.x *= -1;
            araña.transform.localScale = escala;
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
                    audioSource.PlayOneShot(deathSound);
                    audioSource.mute = false;
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
            if (araña != null)
            {
                Vector3 rotation = araña.transform.rotation.eulerAngles;
                rotation.z = 270; // Rotar 180 grados alrededor del eje X
                araña.transform.rotation = Quaternion.Euler(rotation);
            }

            isFlipped = true;
        }

        isFalling = true; 
        canInteract = false; // Desactiva la interacción para evitar múltiples toques

        if (rb2D != null)
        {
            rb2D.bodyType = RigidbodyType2D.Dynamic; // Cambia el Rigidbody2D a dinámico
        }
    }

    // Método para habilitar la interacción si es necesario
    public void EnableInteraction()
    {
        canInteract = true; // Vuelve a permitir la interacción
    }
}
