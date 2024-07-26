using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UIElements;

public class PlatformMove : MonoBehaviour
{
    [SerializeField] GameObject objetMove;

    [SerializeField] float velocity;

    private Vector3 start;
    private Vector3 end;
    private Vector3 moveDirection;

    private bool flip=false;

    [SerializeField] private GameObject araña;

    // Start is called before the first frame update
    void Start()
    {
        float yPosition = transform.position.y;

        // Establecer las posiciones de los puntos start y end

        start = new Vector3(3, yPosition, transform.position.z);
        end = new Vector3(-3, yPosition, transform.position.z);
        moveDirection = end;
    }

    // Update is called once per frame
    void Update()
    {
        objetMove.transform.position = Vector3.MoveTowards(objetMove.transform.position, moveDirection, velocity * Time.deltaTime);

        if (objetMove.transform.position== end)
        {
            moveDirection = start;
            Flip();
        }
        else if( objetMove.transform.position==start){
            moveDirection = end;
            Flip();
        }
    }

    private void Flip()
    {
        if (araña!=null)
        {
            flip = !flip;
            Vector3 escala = araña.transform.localScale;
            escala.x *= -1;
            araña.transform.localScale = escala;
        }
        
    }
}
