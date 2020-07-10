using UnityEngine;
using System.Collections;

public class SheepSpawner : MonoBehaviour 
{
    public float spawnRange_x = 8f; // Max spawn position
    public float spawnRange_y = 4f;
    public float spawnPeriod = 4.3f; // Time between spawns
    public GameObject sheepPrefab;

	private void Start()
	{
		StartCoroutine(SpawnSheep());
	}

	public IEnumerator SpawnSheep()
	{
		while (true)
		{
			Vector2 spawnPosition = new Vector2(Random.Range(-spawnRange_x, spawnRange_x), Random.Range(-spawnRange_y, spawnRange_y)); // Choose random position in our designated box
			Instantiate(sheepPrefab, spawnPosition, Quaternion.identity);

			yield return new WaitForSeconds(spawnPeriod);
		}
	}
}
