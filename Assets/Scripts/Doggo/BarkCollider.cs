using UnityEngine;

public class BarkCollider : MonoBehaviour 
{
	Rigidbody2D body;
	Vector2 dogposition;

	public float velocity;

	void Start ()
	{
	   	body = GetComponent<Rigidbody2D>();
	}

	public void getPos(Vector2 position){
		dogposition = position;
	}

	private void OnTriggerEnter2D(Collider2D collision) // Should this live here or on the sheep/wolf?
	{
		if (collision.gameObject.CompareTag("Sheep"))
		{
			SheepController sheep = collision.gameObject.GetComponent<SheepController>();
			if (sheep) { 
				sheep.UnpanicSheep(); 
				sheep.ChangeVelocity(dogposition, velocity); 

				} // unpanic sheep from dashjump
		}
		else if (collision.gameObject.CompareTag("Wolf"))
		{
			WolfController wolf = collision.gameObject.GetComponent<WolfController>();
			wolf.Die();
		}
	}
}
