using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{


    public void StartGame()
    {

    	GameObject gameManager = GameObject.Find("Game Manager");
        gameManager.GetComponent<GameManager>().startGame();
    }
}
