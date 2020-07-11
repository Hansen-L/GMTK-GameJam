using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueScript : MonoBehaviour
{

	public GameObject animalSpawner;
    public void Continue()
    {
        Time.timeScale = 1;
        animalSpawner.GetComponent<SheepSpawner>().SpawnASheep();
        animalSpawner.GetComponent<SheepSpawner>().SpawnASheep();
        animalSpawner.GetComponent<SheepSpawner>().SpawnASheep();
        animalSpawner.GetComponent<SheepSpawner>().SpawnASheep();
        animalSpawner.GetComponent<SheepSpawner>().SpawnASheep();
        Destroy(this.transform.parent.gameObject);
    }
}
