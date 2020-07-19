using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedTint : MonoBehaviour
{
    public SheepSpawner sheepSpawner;
    private Image image;
    public float health;

    // Start is called before the first frame update
    void Start()
    {
        sheepSpawner = GameObject.Find("Animal Spawner").GetComponent<SheepSpawner>();
        image = this.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        health = (float)sheepSpawner.panickedSheepList.Count / (float)sheepSpawner.sheepList.Count;
        var tempColor = image.color;
        tempColor.a = 0f;

        if (health > 0.5)
        {
            tempColor.a = 0f + (health - 0.5f)/2;
        }

        image.color = tempColor;
    }
}
