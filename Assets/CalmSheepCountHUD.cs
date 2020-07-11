using UnityEngine;
using TMPro;

public class CalmSheepCountHUD : MonoBehaviour 
{
    public TextMeshProUGUI countText;
    public SheepSpawner sheepSpawner;

    void Start() 
    {
        sheepSpawner = GameObject.Find("Animal Spawner").GetComponent<SheepSpawner>();
        countText = GetComponent<TextMeshProUGUI>();
    }

    void Update() 
    {
        countText.text = sheepSpawner.sheepList.Count.ToString() + " Baahs";
    }
}
