using UnityEngine;

public class GameManager : MonoBehaviour 
{
    public int gameOverPercent = 90;
    public SheepSpawner sheepSpawner;
    public float percentPanicked;
    public GameObject gameOverPrefab;
    public GameObject stageCompletePrefab;
    public GameObject stage2CompletePrefab;
    public GameObject stage3CompletePrefab;
    public GameObject stage4CompletePrefab;
    public GameObject endgamePrefab;
    public int stage;
    public bool gaming;

    public float timer;
    private bool gameEnded = false;

    void Start() 
    {
        sheepSpawner = GameObject.Find("Animal Spawner").GetComponent<SheepSpawner>();
        timer = 30;
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
        percentPanicked = (float) sheepSpawner.panickedSheepList.Count / (float) sheepSpawner.sheepList.Count;

        if (Input.GetMouseButtonDown(0) && gaming == false) {
            startGame();
        }
        if (gaming == true && gameEnded == false){

         	timer -= Time.deltaTime;   
        	if (timer < 0){
                stage +=1;
                timer = 30;
                if (stage == 1){
            		GameObject stageComplete = Instantiate(stageCompletePrefab, new Vector3(0, 0), Quaternion.identity);
                    Time.timeScale = 0;
                }
                if (stage == 2){
                    GameObject stageComplete = Instantiate(stage2CompletePrefab, new Vector3(0, 0), Quaternion.identity);
                    Time.timeScale = 0;
                }
                if (stage == 3){
                    GameObject stageComplete = Instantiate(stage3CompletePrefab, new Vector3(0, 0), Quaternion.identity);
                    Time.timeScale = 0;
                }
                if (stage == 4){
                    GameObject stageComplete = Instantiate(stage4CompletePrefab, new Vector3(0, 0), Quaternion.identity);
                    Time.timeScale = 0;
                }
                if (stage >= 5){
                    GameObject stageComplete = Instantiate(endgamePrefab, new Vector3(0, 0), Quaternion.identity);
                    Time.timeScale = 0;
                }
        		
        	}


            if (percentPanicked > gameOverPercent && !gameEnded)
            {
                Instantiate(gameOverPrefab, new Vector3(0, 0), Quaternion.identity);
                gameEnded = true;
            }

        }
    }
}
