using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    private bool spawnerActive;

    private float spawnAngle;

    [Space]
    public float spawnRateMin;
    public float spawnRateMax;

    [Space]
    public float fastTwixSpawnRateMin = 0;
    public float fastTwixSpawnRateMax = 0.1f;

    [Space]
    public int numberObjecstMin = 1;
    public int numberObjectsMax = 1;

    [Space]
    public float objSpeedMin = 10;
    public float objSpeedMax = 10;

    [Space]
    public float spawnRadius = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        spawnAngle = GetComponent<Light>().spotAngle;
        GetComponent<Light>().enabled = false;

        SetSpawnerActive(true);
    }

    private void SpawnObject()
    {
        Vector3 spawnDir = RandomSpawnVector();
        GameObject objPrefab = ObjectSpawnManager.instance.GetRandomObject();
        Vector3 spawnPos = transform.position + (Random.insideUnitSphere * spawnRadius);
        GameObject obj = Instantiate(objPrefab, spawnPos, Random.rotation);
        obj.GetComponent<Rigidbody>().AddForce(spawnDir * Random.Range(objSpeedMin, objSpeedMax), ForceMode.VelocityChange);
    }

    IEnumerator ISpawnObjects()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(spawnRateMin, spawnRateMax));

            // SPAWN OBJECTS
            int numSpawned = Random.Range(numberObjecstMin, numberObjectsMax + 1);
            for (; numSpawned > 0; --numSpawned)
            {
                SpawnObject();
                yield return new WaitForSeconds(Random.Range(fastTwixSpawnRateMin, fastTwixSpawnRateMax));
            }
        }
    }


    public void SetSpawnerActive(bool active)
    {
        spawnerActive = active;
        if (active)
        {
            StartCoroutine(ISpawnObjects());
        } else
        {
            StopAllCoroutines();
        }
    }


    Vector3 RandomSpawnVector()
    {
        float radius = Mathf.Tan(Mathf.Deg2Rad * spawnAngle / 2);
        Vector2 circle = Random.insideUnitCircle * radius;
        Vector3 target = (transform.position + transform.forward) + (transform.rotation * new Vector3(circle.x, circle.y));
        return (target - transform.position).normalized;
    }
}
