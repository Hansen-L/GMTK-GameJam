using UnityEngine;
using TMPro;

public class CalmSheepCountHUD : MonoBehaviour 
{
    public TextMeshProUGUI countText;

    void Start() 
    {
        
    }

    void Update() 
    {
        GameObject gm = GameObject.Find("Game Manager");
        int stage = gm.GetComponent<GameManager>().stage + 1;
        countText.text = "Stage " + stage.ToString();
    }
}
