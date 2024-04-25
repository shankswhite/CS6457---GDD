using System.Collections;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    public GameObject[] rockPrefabs; // Array to hold the rock prefabs.
    public float spawnInterval = 1.5f; // Interval between each spawn.
    public float minSpawnPositionX = -2f; // Minimum x position to spawn a rock.
    public float maxSpawnPositionX = 2f; // Maximum x position to spawn a rock.
    public Vector2 forceRange = new Vector2(10f, 20f); // Range of force to apply to the rocks.

    private void Start()
    {
        StartCoroutine(SpawnRocks());
    }

    private IEnumerator SpawnRocks()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            int randomRockIndex = Random.Range(0, rockPrefabs.Length); // Randomly select a rock prefab.
            Vector3 spawnPosition = new Vector3(transform.position.x + Random.Range(minSpawnPositionX, maxSpawnPositionX), transform.position.y, transform.position.z);

            GameObject spawnedRock = Instantiate(rockPrefabs[randomRockIndex], spawnPosition, Quaternion.identity); // Spawn the rock.

            Rigidbody rb = spawnedRock.GetComponent<Rigidbody>();
            if (rb)
            {
                Vector3 forceDirection = Vector3.down + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;                 // Randomize force direction within a cone shape
                float forceMagnitude = Random.Range(forceRange.x, forceRange.y);
                rb.AddForce(forceDirection * forceMagnitude, ForceMode.Impulse); // Apply force to the rock.
            }
        }
    }

}
