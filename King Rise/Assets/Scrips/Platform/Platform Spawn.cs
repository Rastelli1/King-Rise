using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformSpawn : MonoBehaviour
{
    [SerializeField] List<GameObject> obstacle = new List<GameObject>();
    private List<GameObject> activePlatforms = new List<GameObject>();

    [SerializeField] Transform camTransform;

    [SerializeField] float spawnInterval = 1f; // Distancia constante entre plataformas
    [SerializeField] float despawnDistance = 10f; // Distancia por debajo del jugador para eliminar plataformas
    private float nextSpawnY; // Altura en Y donde se generará la próxima plataforma



    void Start()
    {
        nextSpawnY = camTransform.position.y + spawnInterval - 8; // Inicializar la primera posición de generación
        InitializePlatforms();
    }

    void Update()
    {
        Spawner();
    }

    void SpawnPlatform()
    {
        int obstacles = Random.Range(0, obstacle.Count);
        Vector3 spawnPosition = new Vector3(Random.Range(-3, 3), nextSpawnY + 10, 0); // Genera las plataforma en una posición aleatoria en el eje X
        GameObject newPlatform = Instantiate(obstacle[obstacles], spawnPosition, Quaternion.identity); // Instanciar la plataforma
        activePlatforms.Add(newPlatform);
        nextSpawnY += spawnInterval;
    }
    void InitializePlatforms()
    {
        // Generar plataformas en la posición inicial del jugador
        for (float y = camTransform.position.y; y < nextSpawnY + spawnInterval; y += spawnInterval)
        {
            int obstacles = Random.Range(0, obstacle.Count);
            Vector3 spawnPosition = new Vector3(Random.Range(-3, 3), nextSpawnY, 0); // Generar la plataforma en una posición aleatoria en el eje X
            GameObject newPlatform = Instantiate(obstacle[obstacles], spawnPosition, Quaternion.identity); // Instanciar la plataforma
            activePlatforms.Add(newPlatform);
        }
    }

    void DespawnPlatforms()
    {
        // En esta lista se almacenara los objetos que sera eliminados despues 
        List<GameObject> platformsToRemove = new List<GameObject>();

        foreach (GameObject platform in activePlatforms)
        {
            if (platform.transform.position.y < camTransform.position.y - despawnDistance)
            {
                platformsToRemove.Add(platform);
            }
        }

        // Cuando plataformas esten fuera de vista seran eliminadas 
        foreach (GameObject platform in platformsToRemove)
        {
            activePlatforms.Remove(platform);
            Destroy(platform);
        }
    }

    private void Spawner()
    {
        if (camTransform.position.y + spawnInterval > nextSpawnY) // Si el jugador se acerca a la próxima posición de generación
        {
            SpawnPlatform();
            nextSpawnY += spawnInterval; // Actualizar la altura para la próxima generación
        }
        DespawnPlatforms();
    }
}
