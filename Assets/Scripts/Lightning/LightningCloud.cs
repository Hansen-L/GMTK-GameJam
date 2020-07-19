using UnityEngine;
using System.Collections.Generic;
using Utils;
using System.Collections;

public class LightningCloud : MonoBehaviour 
{
    public float sheepHitVelocity = 80f; // speed sheep runs after hit
    public float timeBeforeStrike = 5f;
    public GameObject lightningEffectPrefab;

    private Animator animator;
    private SpriteRenderer cloudRenderer;
    private PolygonCollider2D cloudCollider;

    void Start() 
    {
        cloudRenderer = this.GetComponent<SpriteRenderer>();
        animator = this.GetComponent<Animator>();
        cloudCollider = this.GetComponent<PolygonCollider2D>();

        StartCoroutine(SpawnLightning());
    }


    public IEnumerator SpawnLightning()
    {
        yield return new WaitForSeconds(timeBeforeStrike);
        Vector2 effectPosition = new Vector2(transform.position.x + 0.208f, transform.position.y - 4.712f);
        GameObject lightningEffectInstance = Instantiate(lightningEffectPrefab, effectPosition, Quaternion.identity);
        yield return new WaitForSeconds(0.13f); // Time for animation to play
        cloudCollider.enabled = true;

        animator.SetBool("cloudDisappear", true);
        yield return new WaitForSeconds(0.1f); // how long to leave the lightning collider
        cloudCollider.enabled = false;
        // play animation to make cloud invasible
        Destroy(gameObject, 5f);
    }

    public void DespawnCloud()
    {
        animator.SetBool("cloudDisappear", true);
        cloudCollider.enabled = false;
        // play animation to make cloud invasible
        Destroy(gameObject, 5f);
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
            else if (collision.gameObject.CompareTag("Wolf"))
            {
                WolfController wolf = collision.gameObject.GetComponent<WolfController>();
                wolf.Die();
            }
        }
    }
}
