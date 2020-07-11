using UnityEngine;

public class DashJumpCollider : MonoBehaviour 
{

	Rigidbody2D body;
	Vector2 location;

	void Start ()
	{
	   	body = GetComponent<Rigidbody2D>();
	   	location = body.transform.position;
	}

	private void OnTriggerEnter2D(Collider2D collision) // Should this live here or on the sheep/wolf?
	{
		if (collision.gameObject.CompareTag("Sheep"))
		{
			SheepController sheep = collision.gameObject.GetComponent<SheepController>();
			if (sheep) { 
				sheep.UnpanicSheep(); 
				sheep.ChangeVelocity(location); 

				} // unpanic sheep from dashjump
		}
		else if (collision.gameObject.CompareTag("Wolf"))
		{
			WolfController wolf = collision.gameObject.GetComponent<WolfController>();
			wolf.Die();
		}
	}
}
