using TMPro;
using UnityEngine;
using System.Collections;

public class ScoreHUD : MonoBehaviour
{
    public int scoreUpdatePeriod = 5; // update every x seconds
    public int scoreMult = 100; // score is updated by numUnpanicked * scoreMult
    public TextMeshProUGUI scoreText; // what percent of sheep are panicked
    public SheepSpawner sheepSpawner;

    private float originalSize;
    private Color originalColor;
    private int score = 0;

    void Start()
    {
        sheepSpawner = GameObject.Find("Animal Spawner").GetComponent<SheepSpawner>();
        scoreText = GetComponent<TextMeshProUGUI>();

        originalSize = scoreText.fontSize;
        originalColor = scoreText.color;

        StartCoroutine(CalculateScore());
    }

    private IEnumerator CalculateScore()
    {
        while (true)
        {
            yield return new WaitForSeconds(scoreUpdatePeriod);

            score += (int) Mathf.Pow((float) sheepSpawner.calmSheepList.Count, 2f) * scoreMult; // increment score
            scoreText.text = score.ToString();
            StartCoroutine(IncreaseScoreAnim());
        }
    }

    private IEnumerator IncreaseScoreAnim() // Animate the damage update
    {
        float duration = 0.2f; // Animation total duration
        float step = 0.04f; // Step between animation frames

        for (int i = 0; i <= duration / step; i++)
        {
            scoreText.fontSize = (int) (originalSize + i * 2);
            //scoreText.color = new Color(0.95f + Random.Range(-0.5f, 0.05f), 0f, 0f);
            yield return new WaitForSeconds(step);
        }

        scoreText.fontSize = originalSize;
        scoreText.color = originalColor;
    }
}
