using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnManager : MonoBehaviour
{
    public static ObjectSpawnManager instance;

    public GameObject[] spawnablePrefabs;

    public void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    public GameObject GetRandomObject()
    {
        return spawnablePrefabs[Random.Range(0, spawnablePrefabs.Length)];
    }
}
