using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Rigidbody2D rb2D;

    private float movimientohorizontal = 0f;

    [SerializeField] private float velocityMove;
    [SerializeField] private float moveSuavizado;

    private Vector3 velocity= Vector3.zero;

    private bool lookingRight = true;

    // salto

    [SerializeField] private float jumpForce ;

    [SerializeField] private LayerMask whatIsFloor;

    [SerializeField] private Vector3 boxDimensions;

    [SerializeField] private Transform controllerFloor;

    [SerializeField] private bool inFloor;
    private bool isOnMovingPlatform = true;
    public bool isJumping = false;

    private bool jump=false;

    [Header("Aimaciones")]

    private Animator animator;

    [Header("Particulas")]

    [SerializeField] private ParticleSystem particulas;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movimientohorizontal = Input.GetAxisRaw("Horizontal") * velocityMove;
        animator.SetFloat("Horizontal", Mathf.Abs( movimientohorizontal ) );
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
        animator.SetBool("InFloor", inFloor);
        inFloor = Physics2D.OverlapBox(controllerFloor.position, boxDimensions, 0f, whatIsFloor);

        

        Mover(movimientohorizontal*Time.fixedDeltaTime, jump);
    }

    private void Mover(float mover, bool jumping)
    {
        Vector3 velocityObjetivo = new Vector2(mover, rb2D.velocity.y);
        rb2D.velocity= Vector3.SmoothDamp(rb2D.velocity, velocityObjetivo, ref velocity, moveSuavizado);

        if(mover> 0 && !lookingRight)
        {
            //girar
            Girar();
        }
        else if(mover< 0 && lookingRight)
        {
            Girar();
        }
        if(inFloor && jumping)
        {
            inFloor = false;
            rb2D.AddForce(new Vector2(0f, jumpForce));
        }
        jump = false;
    }

    private void Girar()
    {
        lookingRight=!lookingRight;
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
            // Si el personaje no está en un salto potenciado, permite el salto
            rb2D.AddForce(new Vector2(0f, jumpForce * 2));
            isJumping = true; // Marca que el personaje está en un salto potenciado
            isOnMovingPlatform = true; // Marca que el personaje está en una plataforma móvil
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlatformMove") )
        {
            isOnMovingPlatform = true;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Object") )
        {
            GameManager.instance.GameOver();
        }
    }



}
