using UnityEngine;

public class GameManager : MonoBehaviour 
{
    public int gameOverPercent = 90;
    public SheepPercentHUD sheepPrecentHUD;
    public GameObject gameOverPrefab;
    public GameObject stageCompletePrefab;
    public int stage;
    public bool gaming;

    public float timer;
    private bool gameEnded = false;

    void Start() 
    {
        sheepPrecentHUD = GameObject.Find("Percent").GetComponent<SheepPercentHUD>();
        timer = 60;
        stage = 0;
    }

    public void startGame(){
        gaming = true;
        GameObject canvas = GameObject.Find("Player Canvas");
        canvas.GetComponent<Canvas>().enabled = true;
        GameObject startcanvas = GameObject.Find("Game Start Canvas");
        startcanvas.GetComponent<Canvas>().enabled = false;
    }


    void Update() 
    {
        if (Input.GetKeyDown("space") && gaming == false) {
            startGame();
        }
        if (gaming == true){

         	timer -= Time.deltaTime;   
        	if (timer < 0){
        		GameObject stageComplete = Instantiate(stageCompletePrefab, new Vector3(0, 0), Quaternion.identity);
        		stage +=1;
        		timer = 60;
        		Time.timeScale = 0;
        	}


            if (sheepPrecentHUD.percentPanicked > gameOverPercent && !gameEnded)
            {
                Instantiate(gameOverPrefab, new Vector3(0, 0), Quaternion.identity);
                gameEnded = true;
            }

        }
    }
}
