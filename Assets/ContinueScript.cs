using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueScript : MonoBehaviour
{

	public GameObject animalSpawner;
	public GameObject lightningSpawner;

	private void Awake()
	{
        animalSpawner = GameObject.Find("Animal Spawner");
        lightningSpawner = GameObject.Find("Lightning Spawner");
	}

	public void Continue()
    {
        Time.timeScale = 1;
        animalSpawner.GetComponent<SheepSpawner>().SpawnASheep(10);
        animalSpawner.GetComponent<SheepSpawner>().MakeSheepCalm();
        animalSpawner.GetComponent<WolfSpawner>().KillAllWolves();
        lightningSpawner.GetComponent<LightningSpawner>().KillAllLightning();
        
        Destroy(this.transform.parent.gameObject);
    }
}
