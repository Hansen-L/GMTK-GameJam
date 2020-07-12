﻿using TMPro;
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

        if (GetComponent<TextMeshProUGUI>().text == "+SPEED"){
            GameObject.Find("Doggo").GetComponent<PlayerMovement>().max_speed = 15;
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
            animalSpawner.GetComponent<SheepSpawner>().SpawnASheep(20);
        }
        if (GetComponent<TextMeshProUGUI>().text == "BIGGER BORK"){
            GameObject.Find("Doggo").GetComponent<PlayerMovement>().bigger_bork = true;
        }
        if (GetComponent<TextMeshProUGUI>().text == "SHADOW DOG"){

        }
        if (GetComponent<TextMeshProUGUI>().text == "MEGA SHEEPS"){
            GameObject.Find("Animal Spawner").GetComponent<SheepSpawner>().bigger = true;
            GameObject.Find("Animal Spawner").GetComponent<SheepSpawner>().MakeSheepSmall();
            animalSpawner.GetComponent<SheepSpawner>().SpawnASheep(4);
        }

        Time.timeScale = 1;
        if (GameObject.Find("Animal Spawner").GetComponent<SheepSpawner>().bigger == false){
            animalSpawner.GetComponent<SheepSpawner>().SpawnASheep(10);
        } else {
            animalSpawner.GetComponent<SheepSpawner>().SpawnASheep(1);
        }
        animalSpawner.GetComponent<SheepSpawner>().MakeSheepCalm();
        animalSpawner.GetComponent<WolfSpawner>().KillAllWolves();
        lightningSpawner.GetComponent<LightningSpawner>().KillAllLightning();
        
        Destroy(this.transform.parent.gameObject);
    }
}
