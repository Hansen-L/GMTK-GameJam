using UnityEngine;

public class DashJumpCollider : MonoBehaviour 
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		SheepController sheep = collision.gameObject.GetComponent<SheepController>();
		if (sheep) { sheep.UnpanicSheep(); } // unpanic sheep from dashjump
	}
}
