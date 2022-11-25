using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreGameSpawner : MonoBehaviour
{
    public SpawnerObject[] spawnerObjects;
    public float spawnRange;
    public bool spawnerEnabled;
    public int amount;
    public bool hasSpawned;

    private void Awake()
    {
        hasSpawned = false;
    }

    private void Update()
    {
        if (!hasSpawned && spawnerEnabled)
        {
            Fire();
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    private void Fire()
    {
        for (int i = 0; i < amount; i++)
        {
            SpawnEntity();
        }
        hasSpawned = true;
    }

    private void SpawnEntity()
    {
        GameObject randomObject = PickRandomObject().prefab;
        if (ObjectPooler.instance.SpawnFromPool(randomObject, RandomNewSpawnPosition(), this.transform.rotation) == null)
        {
            Debug.LogWarning("Something went wrong. Object Pooler couldn't Spawn " + randomObject);
        }
    }

    private SpawnerObject PickRandomObject()
    {
        int totalWeight = 0;
        foreach (SpawnerObject spawnerObject in spawnerObjects)
        {
            totalWeight += spawnerObject.weight;
        }

        SpawnerObject objectToSpawn = null;

        int randomNumber = Random.Range(0, totalWeight);

        foreach (SpawnerObject spawnerObject in spawnerObjects)
        {
            if (randomNumber < spawnerObject.weight)
            {
                objectToSpawn = spawnerObject;
                break;
            }
            randomNumber -= spawnerObject.weight;
        }

        return objectToSpawn;

    }

    private Vector3 RandomNewSpawnPosition()
    {
        float randomPosX = Random.Range(-spawnRange, spawnRange);
        float randomPosZ = Random.Range(-spawnRange, spawnRange);
        return new Vector3(this.transform.position.x + randomPosX, this.transform.position.y, this.transform.position.z + randomPosZ);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, spawnRange);
    }
}
