using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    [SerializeField] List<GameObject> obstacles = new List<GameObject>();

    [SerializeField] float drag;
    [SerializeField] float timeIntervalo;

    [SerializeField] Transform pointEjeY;

    void Start()
    {
        SetDragValue(drag); // esto hara que caigan los obstaculos a la misma velocidad

        InvokeRepeating("PositionGenerate", 0f, timeIntervalo); // esto invocara a la funcion en intervalos de tiempo---> InvokeRepeating("metodo", Tiempo inicial, Intervalo en segundos antes del proximo llamado)
    }
    private void SetDragValue(float newDrag)
    {
        for(int i=0; i<obstacles.Count; i++)
        {
            Rigidbody2D rb = obstacles[i].GetComponent<Rigidbody2D>() ;

            if(rb != null)
            {
                rb.drag = newDrag;
            }
        }
    }

    public void PositionGenerate() //genera una posicion aleatoria en el eje x
    {
        float spawnPositionX= Random.Range(-3.6f , 3.5f);
        Vector3 randompos= new Vector3 (spawnPositionX, pointEjeY.position.y);
        Instantiate(ObstaclesRandom(), randompos, Quaternion.identity);
    }
    public GameObject ObstaclesRandom()
    {
        int obstacle=Random.Range(0, obstacles.Count);

        return obstacles[obstacle];
    }
}
