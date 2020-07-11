using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueScript : MonoBehaviour
{

	public GameObject animalSpawner;

	private void Awake()
	{
        animalSpawner = GameObject.Find("Animal Spawner");
	}

	public void Continue()
    {
        Time.timeScale = 1;
        animalSpawner.GetComponent<SheepSpawner>().SpawnASheep(10);
        animalSpawner.GetComponent<SheepSpawner>().MakeSheepCalm();
        animalSpawner.GetComponent<WolfSpawner>().KillAllWolves();
        Destroy(this.transform.parent.gameObject);
    }
}
