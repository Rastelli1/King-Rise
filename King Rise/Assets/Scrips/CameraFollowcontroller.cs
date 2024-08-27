using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraFollowController : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineVirtualCamera;

    void Start()
    {
        StartCoroutine(FindAndAssignTarget());
    }

    private IEnumerator FindAndAssignTarget()
    {
        // Espera un peque�o retraso para asegurarse de que los objetos est�n cargados
        yield return new WaitForSeconds(0.1f);

        // Busca el primer objeto con la etiqueta "Player"
        GameObject target = GameObject.FindGameObjectWithTag("Player");

        if (target != null)
        {
            cinemachineVirtualCamera.Follow = target.transform;
            Debug.Log($"Asignando '{target.name}' como Follow.");
        }
        else
        {
            Debug.LogWarning("No se encontr� ning�n objeto con la etiqueta 'Player'.");
        }
    }
}
