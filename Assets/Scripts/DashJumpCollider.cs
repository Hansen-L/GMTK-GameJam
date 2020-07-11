using UnityEngine;

public class DashJumpCollider : MonoBehaviour 
{
	private void OnCollisionEnter2D(Collision2D collision) // Should this live here or on the sheep/wolf?
	{
		if (collision.gameObject.CompareTag("Sheep"))
		{
			SheepController sheep = collision.gameObject.GetComponent<SheepController>();
			if (sheep) { sheep.UnpanicSheep(); } // unpanic sheep from dashjump
		}
		else if (collision.gameObject.CompareTag("Wolf"))
		{
			WolfController wolf = collision.gameObject.GetComponent<WolfController>();
			wolf.Die();
		}
	}
}
