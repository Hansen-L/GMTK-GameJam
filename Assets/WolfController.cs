using UnityEngine;

public class WolfController : MonoBehaviour 
{
	
	Vector2 direction;
	public float velocity;

	private Rigidbody2D wolfRb;
	private SpriteRenderer wolfRenderer;

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

	void Start() 
	{
		wolfRb = this.GetComponent<Rigidbody2D>();
		wolfRenderer = this.GetComponent<SpriteRenderer>();
	}

	void FixedUpdate()
	{
		Vector2 newdirection = GetClosestSheep().transform.position - this.transform.position; //breaks if all sheep are panicked I think
		newdirection.Normalize();

		direction.x = Mathf.Lerp(direction.x, newdirection.x, 2*Time.fixedDeltaTime);
		direction.y = Mathf.Lerp(direction.y, newdirection.y, 2*Time.fixedDeltaTime);

		wolfRb.velocity = direction*velocity;
	}

	public void Die()
	{ // add death effect/smoke here
		Destroy(gameObject);
	}

	GameObject GetClosestSheep() 
	{
		GameObject[] sheeps;
		sheeps = GameObject.FindGameObjectsWithTag("Sheep");
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject go in sheeps) // very inefficient, hopefully we don't have to care though (might have to though, this is called ~60 times a second...)
		{
			if (go.GetComponent<SheepController>().isPanicked == false) // wolf should only hunt unpanicked sheep
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
