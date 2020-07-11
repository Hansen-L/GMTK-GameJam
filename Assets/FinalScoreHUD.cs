using UnityEngine;
using TMPro;

public class FinalScoreHUD : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;
    public SheepSpawner sheepSpawner;

    void Start()
    {
        finalScoreText = GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        string finalScore = scoreText.text;
        finalScoreText.text = finalScore; // final score is set to current score

        Debug.Log("test");
    }
}
