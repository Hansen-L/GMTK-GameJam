using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using System.Collections;

public class SheepPercentHUD : MonoBehaviour 
{
    public TextMeshProUGUI percentPanickedText; // what percent of sheep are panicked
    public SheepSpawner sheepSpawner;
    public int percentPanicked;

    private float originalSize;
    private Color originalColor;
    private int prevPercentPanicked = 0;

    void Start() 
    {
        sheepSpawner = GameObject.Find("Animal Spawner").GetComponent<SheepSpawner>();
        percentPanickedText = GetComponent<TextMeshProUGUI>();

        originalSize = percentPanickedText.fontSize;
        originalColor = percentPanickedText.color;
    }

    void Update() 
    {
        percentPanicked = (int) ( (float) sheepSpawner.panickedSheepList.Count / (float) sheepSpawner.sheepList.Count * 100);

        if (percentPanicked != prevPercentPanicked)
        {
            if (percentPanicked > prevPercentPanicked) { StartCoroutine(IncreasePercentAnim()); }
            else { StartCoroutine(DecreasePercentAnim()); }

            percentPanickedText.text = percentPanicked + "%";
        }

        prevPercentPanicked = percentPanicked;
    }

    private IEnumerator IncreasePercentAnim() // Animate the damage update
    {
        float duration = 0.2f; // Animation total duration
        float step = 0.02f; // Step between animation frames

        for (int i = 0; i <= duration / step; i++)
        {
            percentPanickedText.fontSize = originalSize + i / 2 + Random.Range(-2, 2);
            percentPanickedText.color = new Color(0.95f + Random.Range(-0.5f, 0.05f), 0f, 0f);
            yield return new WaitForSeconds(step);
        }

        percentPanickedText.fontSize = originalSize;
        percentPanickedText.color = originalColor;
    }

    private IEnumerator DecreasePercentAnim() // Animate the damage update
    {
        float duration = 0.2f; // Animation total duration
        float step = 0.02f; // Step between animation frames

        for (int i = 0; i <= duration / step; i++)
        {
            percentPanickedText.fontSize = originalSize + i / 6 + Random.Range(-1, 1);
            percentPanickedText.color = new Color(0f, 0.95f + Random.Range(-0.2f, 0.05f), 0f);
            yield return new WaitForSeconds(step);
        }

        percentPanickedText.fontSize = originalSize;
        percentPanickedText.color = originalColor;
    }
}
