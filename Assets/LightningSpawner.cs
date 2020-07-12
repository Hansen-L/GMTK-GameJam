using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LightningSpawner : MonoBehaviour 
{
	public float spawnPeriod = 3f; // Time between spawns
	public GameObject lightningPrefab;

	private float boundary_x;
	private float boundary_y;

	private void Start()
	{
		GameObject boundaryObj = GameObject.Find("Boundaries");
		BoundaryNumbers boundary = boundaryObj.GetComponent<BoundaryNumbers>();

		boundary_x = boundary.playerBoundary_x - 2;
		boundary_y = boundary.playerBoundary_y;

		StartCoroutine(SpawnLightning());
	}

	void Update()
	{
		GameObject gameManager;
		gameManager = GameObject.Find("Game Manager");
		spawnPeriod = gameManager.GetComponent<GameManager>().timer/(gameManager.GetComponent<GameManager>().stage+6)+0.5f;
		
	}

	public IEnumerator SpawnLightning()
	{
		while (true)
		{
			yield return new WaitForSeconds(spawnPeriod);
			GameObject gameManager;
			gameManager = GameObject.Find("Game Manager");
			if (gameManager.GetComponent<GameManager>().stage > 0 && gameManager.GetComponent<GameManager>().timer > 4 && gameManager.GetComponent<GameManager>().timer < 55){
				Vector2 spawnPosition = new Vector2(Random.Range(-boundary_x, boundary_x), Random.Range(-boundary_y + 7, boundary_y + 5)); // Choose random position in our designated box
				GameObject lightningInstance = Instantiate(lightningPrefab, spawnPosition, Quaternion.identity);
				Destroy(lightningInstance, 10f);
			}
		}
	}

	public void KillAllLightning() // Can be called from other functions
	{
		GameObject[] lightningList = GameObject.FindGameObjectsWithTag("Lightning");
		foreach (GameObject lightning in lightningList)
		{
			StartCoroutine(lightning.GetComponent<LightningCloud>().CloudDisappear()); // Make clouds fade out
		}
	}
}
