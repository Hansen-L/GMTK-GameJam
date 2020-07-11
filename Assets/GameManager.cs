using UnityEngine;

public class GameManager : MonoBehaviour 
{
    public int gameOverPercent = 90;
    public SheepPercentHUD sheepPrecentHUD;
    public GameObject gameOverPrefab;

    private bool gameEnded = false;

    void Start() 
    {
        sheepPrecentHUD = GameObject.Find("Percent").GetComponent<SheepPercentHUD>();
    }


    void Update() 
    {
        if (sheepPrecentHUD.percentPanicked > gameOverPercent && !gameEnded)
        {
            Instantiate(gameOverPrefab, new Vector3(0, 0), Quaternion.identity);
            gameEnded = true;
        }
    }
}
