using UnityEngine;
using System.Collections;

public class WolfSpawner : MonoBehaviour
{
	public float spawnPeriod = 1f; // Time between spawns
	public GameObject wolfPrefab;

	private float boundary_x;
	private float boundary_y;

	private void Start()
	{
		GameObject boundaryObj = GameObject.Find("Boundaries");
		BoundaryNumbers boundary = boundaryObj.GetComponent<BoundaryNumbers>();

		boundary_x = boundary.playerBoundary_x;
		boundary_y = boundary.playerBoundary_y;

		StartCoroutine(SpawnWolves());
	}

	public IEnumerator SpawnWolves() // Spawn wolf outside fence, wolf can walk through fence
	{
		while (true)
		{
			yield return new WaitForSeconds(spawnPeriod);

			if (Random.Range(0f, 1f) > 0.5) // Half the time, spawn on top/bot
			{
				int signFlip = Random.Range(0, 2) * 2 - 1; // either -1 or 1
				float y = (Random.Range(3f, 6f) + boundary_y) * signFlip;
				// Spawn outside fence
				Vector2 spawnPosition = new Vector2(Random.Range(-boundary_x, boundary_x), y); // Choose random position in our designated box
				Instantiate(wolfPrefab, spawnPosition, Quaternion.identity);
			}
			else // spawn on left/right
			{
				int signFlip = Random.Range(0, 2) * 2 - 1; // either -1 or 1
				float x = (Random.Range(3f, 6f) + boundary_x) * signFlip;
				// Spawn outside fence
				Vector2 spawnPosition = new Vector2(x, Random.Range(-boundary_y, boundary_y)); // Choose random position in our designated box
				Instantiate(wolfPrefab, spawnPosition, Quaternion.identity);
			}

		}
	}
}
