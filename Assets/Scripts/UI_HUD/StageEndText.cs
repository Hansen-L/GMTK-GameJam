using TMPro;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class StageEndText : MonoBehaviour
{
    public TextMeshProUGUI stageCompleteText;
    private GameObject audioManagerObj;
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManagerObj = GameObject.Find("Audio Manager");
        audioManager = audioManagerObj.GetComponent<AudioManager>();

        stageCompleteText.text = "STAGE " + GameManager.Instance.stage.ToString() + " COMPLETE";

        audioManager.PlayOneShot("tada");
    }
}
