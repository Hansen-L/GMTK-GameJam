using TMPro;
using System.Collections.Generic;
using UnityEngine;

public class StageScreenScript : MonoBehaviour
{


	public GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Complete").GetComponent<TextMeshProUGUI>().text = "STAGE " + gameManager.GetComponent<GameManager>().stage + " COMPLETE";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
