using UnityEngine;

public class WoldController : MonoBehaviour 
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
		direction = this.transform.position - GetClosestSheep().transform.position;
		direction.Normalize();

		wolfRb.velocity = direction*velocity;
	}

	void Die()
	{

	}

	public GameObject GetClosestSheep()
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
