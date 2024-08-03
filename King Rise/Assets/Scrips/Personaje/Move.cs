using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private float movimientohorizontal = 0f;

    [SerializeField] private float velocityMove;
    [SerializeField] private float moveSuavizado;
    private Vector3 velocity = Vector3.zero;
    private bool lookingRight = true;

    // Salto
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask whatIsFloor;
    [SerializeField] private Vector3 boxDimensions;
    [SerializeField] private Transform controllerFloor;

    [SerializeField] private AnimationClip animacionDead;

    private bool inFloor;
    private bool isOnMovingPlatform;
    public bool isJumping = false;

    private bool jump = false;
    private bool dead = false;

    [Header("Animaciones")]
    private Animator animator;

    [Header("Partículas")]
    [SerializeField] private ParticleSystem particulas;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        
        movimientohorizontal = Input.GetAxisRaw("Horizontal") * velocityMove;
        animator.SetFloat("Horizontal", Mathf.Abs(movimientohorizontal));
        
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            particulas.Play();
        }

        if (rb2D.velocity.y < 0 && isJumping)
        {
            // Permite el contacto con una nueva plataforma móvil sólo si está descendiendo
            isJumping = false;
        }
    }

    private void FixedUpdate()
    {
        inFloor = Physics2D.OverlapBox(controllerFloor.position, boxDimensions, 0f, whatIsFloor);
        animator.SetBool("InFloor", inFloor);

        Mover(movimientohorizontal * Time.fixedDeltaTime, jump);
    }

    private void Mover(float mover, bool jumping)
    {
        if (dead) return;

        Vector3 velocityObjetivo = new Vector2(mover, rb2D.velocity.y);
        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, velocityObjetivo, ref velocity, moveSuavizado);

        if (mover > 0 && !lookingRight)
        {
            Girar();
        }
        else if (mover < 0 && lookingRight)
        {
            Girar();
        }

        if (inFloor && jumping)
        {
            inFloor = false;
            rb2D.velocity = new Vector2(rb2D.velocity.x, 0f); // Reset vertical velocity
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce); // Set consistent jump velocity
        }

        jump = false;
    }

    private void Girar()
    {
        lookingRight = !lookingRight;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(controllerFloor.position, boxDimensions);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlatformMove") && !isJumping)
        {
            isJumping = true;
            isOnMovingPlatform = true;
            rb2D.velocity = new Vector2(rb2D.velocity.x, 0f); // Reset vertical velocity when landing on moving platform
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce + 2); // Set increased jump velocity
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlatformMove"))
        {
            // Asegúrate de que isOnMovingPlatform se restablezca a false cuando el personaje deja la plataforma
            isOnMovingPlatform = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Object"))
        {
            
            StartCoroutine(Dead());
        }
    }
    IEnumerator Dead()
    {
        dead = true;
        animator.SetBool("Dead", true);
        rb2D.velocity = Vector2.zero; 
        rb2D.gravityScale = 0;
        movimientohorizontal = 0f;
        yield return new WaitForSecondsRealtime(animacionDead.length);
        GameManager.instance.GameOver();
    }
}