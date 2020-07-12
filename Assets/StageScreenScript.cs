using TMPro;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class StageScreenScript : MonoBehaviour
{
    private GameObject gameManager;
    public TextMeshProUGUI stageCompleteText;
    private GameObject audioManagerObj;
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManagerObj = GameObject.Find("Audio Manager");
        audioManager = audioManagerObj.GetComponent<AudioManager>();

        gameManager = GameObject.Find("Game Manager");
        stageCompleteText.text = "STAGE " + gameManager.GetComponent<GameManager>().stage.ToString() + " COMPLETE";

        audioManager.PlayOneShot("tada");
    }
}
