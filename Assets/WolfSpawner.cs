using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WolfSpawner : MonoBehaviour
{
	public float spawnPeriod = 1f; // Time between spawns
	public GameObject wolfPrefab;
	//public List<GameObject> wolfList = new List<GameObject>();

	private float boundary_x;
	private float boundary_y;

	private void Awake()
	{
		//wolfList = GameObject.FindGameObjectsWithTag("Wolf").ToList();
	}

	private void Start()
	{
		GameObject boundaryObj = GameObject.Find("Boundaries");
		BoundaryNumbers boundary = boundaryObj.GetComponent<BoundaryNumbers>();

		boundary_x = boundary.playerBoundary_x;
		boundary_y = boundary.playerBoundary_y;

		StartCoroutine(SpawnWolves());
	}

	void Update()
	{
		GameObject gameManager;
		gameManager = GameObject.Find("Game Manager");
		spawnPeriod = gameManager.GetComponent<GameManager>().timer/9;
		
	}

	public IEnumerator SpawnWolves() // Spawn wolf outside fence, wolf can walk through fence
	{
		while (true)
		{
			yield return new WaitForSeconds(spawnPeriod);
			GameObject gameManager;
			gameManager = GameObject.Find("Game Manager");

			if (gameManager.GetComponent<GameManager>().gaming == true){
				if (Random.Range(0f, 1f) > 0.5) // Half the time, spawn on top/bot
				{
					int signFlip = Random.Range(0, 2) * 2 - 1; // either -1 or 1
					float y = (Random.Range(3f, 6f) + boundary_y) * signFlip;
					// Spawn outside fence
					Vector2 spawnPosition = new Vector2(Random.Range(-boundary_x, boundary_x), y); // Choose random position in our designated box
					GameObject wolfInstance = Instantiate(wolfPrefab, spawnPosition, Quaternion.identity);
					//wolfList.Add(wolfInstance);
				}
				else // spawn on left/right
				{
					int signFlip = Random.Range(0, 2) * 2 - 1; // either -1 or 1
					float x = (Random.Range(3f, 6f) + boundary_x) * signFlip;
					// Spawn outside fence
					Vector2 spawnPosition = new Vector2(x, Random.Range(-boundary_y, boundary_y)); // Choose random position in our designated box
					GameObject wolfInstance = Instantiate(wolfPrefab, spawnPosition, Quaternion.identity);
					//wolfList.Add(wolfInstance);
				}
			}

		}
	}

	public void KillAllWolves() // Can be called from other functions
	{
		GameObject[] wolfList = GameObject.FindGameObjectsWithTag("Wolf");
		foreach (GameObject wolf in wolfList){
			wolf.GetComponent<WolfController>().Die();
		}
	}
}
