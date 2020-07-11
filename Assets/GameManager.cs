using UnityEngine;

public class GameManager : MonoBehaviour 
{
    public int gameOverPercent = 90;
    public SheepPercentHUD sheepPrecentHUD;
    public GameObject gameOverPrefab;
    public GameObject stagePrefab;
    public int stage;

    private float timer;
    private bool gameEnded = false;

    void Start() 
    {
        sheepPrecentHUD = GameObject.Find("Percent").GetComponent<SheepPercentHUD>();
        timer = 0;
        stage = 1;
    }


    void Update() 
    {
     	timer += Time.deltaTime;   
    	if (timer > 10){
    		GameObject stageComplete = Instantiate(stagePrefab, new Vector3(0, 0), Quaternion.identity);
    		stage +=1;
    		timer = 0;
    		Time.timeScale = 0;
    	}


        if (sheepPrecentHUD.percentPanicked > gameOverPercent && !gameEnded)
        {
            Instantiate(gameOverPrefab, new Vector3(0, 0), Quaternion.identity);
            gameEnded = true;
        }
    }
}
