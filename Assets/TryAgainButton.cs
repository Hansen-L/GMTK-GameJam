using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgainButton : MonoBehaviour 
{

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainScene");
    }
}
