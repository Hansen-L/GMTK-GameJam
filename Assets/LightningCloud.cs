using UnityEngine;
using System.Collections.Generic;
using Utils;
using System.Collections;

public class LightningCloud : MonoBehaviour 
{
    public float sheepHitVelocity = 80f; // speed sheep runs after hit
    public float timeBeforeStrike = 5f; 

    private Animator animator;
    private SpriteRenderer cloudRenderer;
    private CircleCollider2D cloudCollider;

    void Start() 
    {
        cloudRenderer = this.GetComponent<SpriteRenderer>();
        animator = this.GetComponent<Animator>();
        cloudCollider = this.GetComponent<CircleCollider2D>();

        StartCoroutine(SpawnLightning());
    }


    public IEnumerator SpawnLightning()
    {
        yield return new WaitForSeconds(timeBeforeStrike);
        cloudCollider.enabled = true;
        yield return new WaitForSeconds(0.1f); // how long to leave the lightning collider
        cloudCollider.enabled = false;
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        // lightning hits sheep
        if (collision.gameObject.CompareTag("Sheep"))
        {
            SheepController sheep = collision.gameObject.GetComponent<SheepController>();
            if (sheep.isPanicked == false)
            {
                sheep.PanicSheep();
                sheep.ChangeVelocity(transform.position, sheepHitVelocity);
            }
        }
    }
}
