﻿using TMPro;
using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
    public int scoreUpdatePeriod = 5; // update every x seconds
    public int scoreMult = 100; // score is updated by numUnpanicked * scoreMult
    public TextMeshProUGUI timeText; // what percent of sheep are panicked
    public SheepSpawner sheepSpawner;

    private float originalSize;
    private Color originalColor;
    private int time = 0;

    void Start()
    {
        sheepSpawner = GameObject.Find("Animal Spawner").GetComponent<SheepSpawner>();
        timeText = GetComponent<TextMeshProUGUI>();

        originalSize = timeText.fontSize;
        originalColor = timeText.color;

        //StartCoroutine(CalculateScore());
    }


    void Update(){ // update time
        int prevTime = time;
        time = (int) GameManager.Instance.timer;
        timeText.text = time.ToString();

        if (prevTime != time && time <= 10){ //time from 10 to 0
            StartCoroutine(IncreaseTimeAnim());
            AudioManager.Instance.PlayVolume("tick", 1f - (float)time/20f); //louder over time
        }
    }

    private IEnumerator IncreaseTimeAnim() // Animate the damage update
    {
        float duration = 0.2f; // Animation total duration
        float step = 0.04f; // Step between animation frames

        for (int i = 0; i <= duration / step; i++)
        {
            timeText.fontSize = (int) (originalSize + i * 3);
            timeText.color = new Color(0.95f + Random.Range(-0.5f, 0.05f), 0f, 0f);
            yield return new WaitForSeconds(step);
        }

        timeText.fontSize = originalSize;
        timeText.color = originalColor;
    }
}
