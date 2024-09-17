using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointPlatformFragil : MonoBehaviour
{
    [SerializeField] GameObject platformFragil;
    [SerializeField] float timeRespawn;
    private bool isRespawning=false;
    // Start is called before the first frame update
    void Start()
    {
        if(transform.childCount == 0 && platformFragil!=null)
        {
            GameObject newChild = Instantiate(platformFragil, transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 0 && platformFragil != null && !isRespawning)
        {
            StartCoroutine(Respawn(timeRespawn));
        }
    }

    private IEnumerator Respawn(float time)
    {
        isRespawning=true;
        yield return new WaitForSeconds(time);
        GameObject newChild = Instantiate(platformFragil, transform);
        isRespawning = false;
    }
}
