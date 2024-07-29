using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFragil : MonoBehaviour
{
    [SerializeField] private float timeDelete;

    [Header("Animaciones")]
    private Animator animator;
    private bool startDestruction = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startDestruction)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("Delete") && stateInfo.normalizedTime >= 1.0f)
            {
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator AfterCollision(float time)
    {
        yield return new WaitForSeconds(time);

        animator.SetBool("Delete", true); // Inicia la animación "Delete"
        startDestruction = true; // Marca que la destrucción ha comenzado
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(AfterCollision(timeDelete));
        }
    }

}
