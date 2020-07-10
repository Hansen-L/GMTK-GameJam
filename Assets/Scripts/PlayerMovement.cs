using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	Rigidbody2D body;

	float horizontal;
	float vertical;
	float h_velocity;
	float v_velocity;

	public float acceleration;
	public float max_speed;
	public float friction;

	void Start ()
	{
	   	body = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
	   	// Gives a value between -1 and 1
	   	horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
	   	vertical = Input.GetAxisRaw("Vertical"); // -1 is down
	}

	void FixedUpdate()
	{

		if (horizontal * h_velocity <= 0){
			h_velocity *= friction;
		}
		if (vertical * v_velocity <= 0){
			v_velocity *= friction;
		}

		h_velocity = Mathf.Clamp(h_velocity + horizontal * acceleration,-max_speed, max_speed);
		v_velocity = Mathf.Clamp(v_velocity + vertical * acceleration,-max_speed, max_speed);

		Vector2 total_velocity = new Vector2(h_velocity, v_velocity);

	   	body.velocity = Vector2.ClampMagnitude(total_velocity, max_speed);

	   	Debug.Log(body.velocity);
	}
}
