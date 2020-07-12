using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SheepSpawner : MonoBehaviour 
{
    public float spawnRange_x = 8f; // Max spawn position
    public float spawnRange_y = 4f;
    public float spawnPeriod = 4.3f; // Time between spawns
    public GameObject sheepPrefab;
    public bool smaller;
    public bool bigger;

	public List<GameObject> sheepList = new List<GameObject>(); // Store all the spawned sheep. Sheep never die
	public List<GameObject> panickedSheepList = new List<GameObject>();
	public List<GameObject> calmSheepList = new List<GameObject>();

	private void Awake()
	{
		sheepList = GameObject.FindGameObjectsWithTag("Sheep").ToList(); // Instantiate sheep list with sheep on screen
	}

	private void Start()
	{
		//StartCoroutine(SpawnSheepCoroutine());
	}

	private void Update()
	{
		// Need to reset these lists so we can repopulate them... fuck this is inefficient :')
		panickedSheepList = new List<GameObject>();
		calmSheepList = new List<GameObject>();

		foreach (GameObject sheep in sheepList)
		{
			if (sheep.GetComponent<SheepController>().isPanicked == true) { panickedSheepList.Add(sheep); }
			else { calmSheepList.Add(sheep); }
		}
	}

	public IEnumerator SpawnSheepCoroutine()
	{
		while (true)
		{
			yield return new WaitForSeconds(spawnPeriod);

			SpawnASheep();
		}
	}

	public void SpawnASheep(int number = 1) // Can be called from other functions
	{
		int i;
		for (i = 0; i < number; i++)
		{
			Vector2 spawnPosition = new Vector2(Random.Range(-spawnRange_x, spawnRange_x), Random.Range(-spawnRange_y, spawnRange_y)); // Choose random position in our designated box
			GameObject newSheep = Instantiate(sheepPrefab, spawnPosition, Quaternion.identity);
			if (smaller == true){
				newSheep.transform.localScale = new Vector2 (2f,2f);
			}
			if (bigger == true){
				newSheep.transform.localScale = new Vector2 (16f,16f);
			}
			sheepList.Add(newSheep);
		}
	}

	public void MakeSheepCalm() // Can be called from other functions
	{
		foreach (GameObject sheep in panickedSheepList){
			sheep.GetComponent<SheepController>().UnpanicSheep();
		}
	}

	public void MakeSheepSmall() // Can be called from other functions (kills all sheep)
	{
		sheepList = new List<GameObject>(); // reset sheep lists
		panickedSheepList = new List<GameObject>();
		calmSheepList = new List<GameObject>();

		GameObject[] sheepArr = GameObject.FindGameObjectsWithTag("Sheep");
		foreach (GameObject sheep in sheepArr){
			if (sheep != null) { sheep.GetComponent<SheepController>().Die(); }
		}
		
	}
}