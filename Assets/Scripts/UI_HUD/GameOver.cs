using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;
    public SheepSpawner sheepSpawner;

    void Start()
    {
        finalScoreText = GetComponent<TextMeshProUGUI>();
        GameObject gm = GameObject.Find("Game Manager");
        string finalScore = gm.GetComponent<GameManager>().stage.ToString();
        finalScoreText.text = finalScore + " out of 5"; // final score is set to current score
    }
}
