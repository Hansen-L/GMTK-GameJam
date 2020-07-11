using TMPro;
using System.Collections.Generic;
using UnityEngine;

public class StageScreenScript : MonoBehaviour
{
	private GameObject gameManager;
    public TextMeshProUGUI stageCompleteText;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager");
        stageCompleteText.text = "STAGE " + gameManager.GetComponent<GameManager>().stage.ToString() + " COMPLETE";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
