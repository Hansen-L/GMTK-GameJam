using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgainButton : MonoBehaviour 
{

    public void Retry()
    {
        SceneManager.LoadScene("MainScene");
    }
}
