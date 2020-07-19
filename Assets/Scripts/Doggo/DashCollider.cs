using UnityEngine;

public class DashCollider : MonoBehaviour 
{
	Rigidbody2D body;
	public float velocity;
	void Start ()
	{
	   	body = GetComponent<Rigidbody2D>();
	}

	private void OnTriggerEnter2D(Collider2D collision) // Should this live here or on the sheep/wolf?
	{
		if (collision.gameObject.CompareTag("Sheep"))
		{
			SheepController sheep = collision.gameObject.GetComponent<SheepController>();
			if (sheep) { 
				sheep.UnpanicSheep(); 
				sheep.ChangeVelocity(transform.position, velocity); 

				} // unpanic sheep from dashjump
		}
		else if (collision.gameObject.CompareTag("Wolf"))
		{
			WolfController wolf = collision.gameObject.GetComponent<WolfController>();
			wolf.Die();
		}
	}
}
