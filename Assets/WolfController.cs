using UnityEngine;
using System.Collections.Generic;
using Utils;

public class WolfController : MonoBehaviour 
{
	
	Vector2 direction;
	public float velocity;

	private Rigidbody2D wolfRb;
	private SpriteRenderer wolfRenderer;
	private SheepSpawner sheepSpawner;
	private int baseLayer;

	void Start() 
	{
		wolfRb = this.GetComponent<Rigidbody2D>();
		wolfRenderer = this.GetComponent<SpriteRenderer>();
		baseLayer = this.GetComponent<SpriteRenderer>().sortingOrder;

		sheepSpawner = GameObject.Find("Animal Spawner").GetComponent<SheepSpawner>();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		// Wolf touches a sheep
		if (collision.gameObject.CompareTag("Sheep"))
		{
			SheepController sheep = collision.gameObject.GetComponent<SheepController>();
			if (sheep.isPanicked == false)
			{
				sheep.PanicSheep();
				this.Die(); // Wolf disappears after touching sheep
			}
		}
	}

	private void Update()
	{
		// Animate wolf
		if (wolfRb.velocity.x > 0) // moving right
		{
			wolfRenderer.flipX = true;
		}
		else if (wolfRb.velocity.x <= 0) // moving left
		{
			wolfRenderer.flipX = false;
		}

		Utils.Utils.SetRenderLayer(gameObject, baseLayer);
	}

	void FixedUpdate()
	{
		GameObject closestSheep = GetClosestSheep();

		if (closestSheep) // if there exists a non-panicked sheep
		{
			Vector2 newdirection = closestSheep.transform.position - this.transform.position;
			newdirection.Normalize();

			direction.x = Mathf.Lerp(direction.x, newdirection.x, 2 * Time.fixedDeltaTime);
			direction.y = Mathf.Lerp(direction.y, newdirection.y, 2 * Time.fixedDeltaTime);

			wolfRb.velocity = direction * velocity;
		}
	}

	public void Die()
	{ // add death effect/smoke here
		wolfRenderer.enabled = false;
		Destroy(gameObject);
	}

	GameObject GetClosestSheep() 
	{
		List<GameObject> calmSheepList;
		calmSheepList = sheepSpawner.calmSheepList; // Wolf only hunts unpanicked sheep
		GameObject closest = null;

		if (calmSheepList.Count != 0) // avoid errors
		{
			float distance = Mathf.Infinity;
			Vector3 position = transform.position;
			foreach (GameObject go in calmSheepList) // very inefficient, hopefully we don't have to care though (might have to though, this is called ~60 times a second...)
			{
				Vector3 diff = go.transform.position - position;
				float curDistance = diff.sqrMagnitude;
				if (curDistance < distance)
				{
					closest = go;
					distance = curDistance;
				}
			}

		}

		return closest;
	}
}
