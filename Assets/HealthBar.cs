using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

	float health;
	public SheepSpawner sheepSpawner;

    // Start is called before the first frame update
    void Start()
    {
        health = 0;
        sheepSpawner = GameObject.Find("Animal Spawner").GetComponent<SheepSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        health = (float) sheepSpawner.panickedSheepList.Count / (float) sheepSpawner.sheepList.Count;
        this.transform.localScale = new Vector3(Mathf.Lerp(this.transform.localScale.x,health, 0.05f), 1f);
    }
}
