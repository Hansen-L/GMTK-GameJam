using UnityEngine;

public class WolfController : MonoBehaviour 
{
	
	Vector2 direction;
	public float velocity;

	private Rigidbody2D wolfRb;
	private SpriteRenderer wolfRenderer;
	

	void Start() 
	{
		wolfRb = this.GetComponent<Rigidbody2D>();
		wolfRenderer = this.GetComponent<SpriteRenderer>();
	}

	void FixedUpdate()
	{
		Vector2 newdirection = GetClosestSheep().transform.position - this.transform.position;
		newdirection.Normalize();

		direction.x = Mathf.Lerp(direction.x, newdirection.x, 2*Time.fixedDeltaTime);
		direction.y = Mathf.Lerp(direction.y, newdirection.y, 2*Time.fixedDeltaTime);

		wolfRb.velocity = direction*velocity;
	}

	void Die()
	{

	}

	GameObject GetClosestSheep()
	{
		GameObject[] sheeps;
		sheeps = GameObject.FindGameObjectsWithTag("Sheep");
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject go in sheeps)
		{
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance)
			{
				closest = go;
				distance = curDistance;
			}
		}
		return closest;
	}
}
