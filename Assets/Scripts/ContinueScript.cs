using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ContinueScript : MonoBehaviour
{

	public GameObject animalSpawner;
	public GameObject lightningSpawner;
    public GameObject shadowDogPrefab;
    public GameManager gameManager;

	private void Awake()
	{
        animalSpawner = GameObject.Find("Animal Spawner");
        lightningSpawner = GameObject.Find("Lightning Spawner");
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
	}

	public void Continue()
    {

        if (GetComponent<TextMeshProUGUI>().text == "+SPEED"){
            GameObject.Find("Doggo").GetComponent<PlayerMovement>().max_speed = 10;
        }
        if (GetComponent<TextMeshProUGUI>().text == "DASH STOMP"){
            GameObject.Find("Doggo").GetComponent<PlayerMovement>().dash_jump = true;
        }
        if (GetComponent<TextMeshProUGUI>().text == "+ BORK STRENGTH"){
            GameObject.Find("Doggo").GetComponent<PlayerMovement>().more_bork = true;
        }
        if (GetComponent<TextMeshProUGUI>().text == "MAGNETIC BORK"){
            GameObject.Find("Doggo").GetComponent<PlayerMovement>().reverse_bork = true;
        }
        if (GetComponent<TextMeshProUGUI>().text == "SMALLER SHEEPS"){
            GameObject.Find("Animal Spawner").GetComponent<SheepSpawner>().smaller = true;
            GameObject.Find("Animal Spawner").GetComponent<SheepSpawner>().MakeSheepSmall();
            animalSpawner.GetComponent<SheepSpawner>().SpawnASheep(200);
        }
        if (GetComponent<TextMeshProUGUI>().text == "BIGGER BORK"){
            GameObject.Find("Doggo").GetComponent<PlayerMovement>().bigger_bork = true;
        }
        if (GetComponent<TextMeshProUGUI>().text == "SHADOW DOG"){
            GameObject shadowDog = Instantiate(shadowDogPrefab, GameObject.Find("Doggo").transform.position, Quaternion.identity);
        }
        if (GetComponent<TextMeshProUGUI>().text == "MEGA SHEEPS"){
            GameObject.Find("Animal Spawner").GetComponent<SheepSpawner>().bigger = true;
            GameObject.Find("Animal Spawner").GetComponent<SheepSpawner>().MakeSheepSmall();
            animalSpawner.GetComponent<SheepSpawner>().SpawnASheep(26);
        }

        Time.timeScale = 1;
        // spawn sheep logic

        //if normal size sheep
        if (GameObject.Find("Animal Spawner").GetComponent<SheepSpawner>().bigger == false && GameObject.Find("Animal Spawner").GetComponent<SheepSpawner>().smaller == false){
            animalSpawner.GetComponent<SheepSpawner>().SpawnASheep(10 * gameManager.stage);
        } else if (GameObject.Find("Animal Spawner").GetComponent<SheepSpawner>().bigger == true){ // bigger sheep
            animalSpawner.GetComponent<SheepSpawner>().SpawnASheep(1 * gameManager.stage);
        } else { // smaller sheep?
            animalSpawner.GetComponent<SheepSpawner>().SpawnASheep(20 * gameManager.stage);
        }
        animalSpawner.GetComponent<WolfSpawner>().KillAllWolves();
        lightningSpawner.GetComponent<LightningSpawner>().KillAllLightning();
        animalSpawner.GetComponent<SheepSpawner>().MakeSheepCalm();
        
        Destroy(this.transform.parent.gameObject);
    }
}
