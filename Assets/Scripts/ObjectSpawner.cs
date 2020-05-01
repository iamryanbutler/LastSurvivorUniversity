using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab;
    public float objectYPosition;
    public bool isRespawnable;
    public bool allowInfiniteSpawns;
    public float respawnTime;
    [Min(1)]
    public int numberOfRespawns;
    float timeUntilNextRespawn;
    bool isFirst;
    int respawnCounter;
    GameObject obj;

    void Start()
    {
        obj = Instantiate(objectPrefab, this.gameObject.transform.position, Quaternion.identity, this.gameObject.transform);
        obj.transform.position += new Vector3(0, objectYPosition, 0);
        numberOfRespawns = numberOfRespawns == 0 ? 1 : numberOfRespawns;
        timeUntilNextRespawn = respawnTime;
        isFirst = true;
        respawnCounter = 0;
    }

    void Update()
    {
        // if the prefabed-object was deleted
        if(this.transform.childCount <= 0)
        {
            if (isRespawnable && ((respawnCounter < numberOfRespawns) || allowInfiniteSpawns))
            {
                if (isFirst)
                {
                    timeUntilNextRespawn = Time.time + respawnTime;
                    isFirst = false;
                }

                // if cooldown has finished, spawn a new object
                else if (Time.time > timeUntilNextRespawn)
                {
                    obj = Instantiate(objectPrefab, this.gameObject.transform.position, Quaternion.identity, this.gameObject.transform);
                    obj.transform.position += new Vector3(0, objectYPosition, 0);
                    isFirst = true;
                    respawnCounter++;
                }
            }
            else
                Destroy(gameObject);
        }
        else
            obj.transform.Rotate(Vector3.up * 150f * Time.deltaTime);
    }
}
