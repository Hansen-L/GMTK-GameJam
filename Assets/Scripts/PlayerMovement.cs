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
	float dash_timer;
	float stun_timer;
	float stun_time;
	Vector2 dash_direction;
	bool dashing;
	bool stunned;

	public float acceleration;
	public float max_speed;
	public float friction;
	public float dash_speed;
	public float dash_time;
	public float dash_end_stun;
	public Camera camera;


	void Start ()
	{
	   	body = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
	   	// Gives a value between -1 and 1
	   	horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
	   	vertical = Input.GetAxisRaw("Vertical"); // -1 is down

	   	if (Input.GetKeyDown("space")){
	   		Dash_start();
	   	}
	}



	void FixedUpdate()
	{

		if (stunned == true){
			stun_timer += Time.fixedDeltaTime;
			body.velocity = new Vector2(0,0);
			if (stun_timer >= stun_time){
				stunned = false;
			}

		}else if(dashing == true){
			dash_timer += Time.fixedDeltaTime;
			body.velocity = dash_speed * dash_direction;

			if (dash_timer >= dash_time){
				Dash_end();
			}

		}else{

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

	void Dash_start()
	{
		dash_timer = 0;
		dashing = true;
		Vector2 mousePos = Input.mousePosition;//gets mouse postion
     	mousePos = camera.ScreenToWorldPoint (mousePos);
		dash_direction =  new Vector2(mousePos.x - body.position.x, mousePos.y - body.position.y);
		dash_direction.Normalize();
	}

	void Dash_end()
	{
		dashing = false;
		body.velocity = new Vector2(0,0);
		stun_time = dash_end_stun;
		Stun_start();
	}

	void Stun_start()
	{
		stun_timer = 0;
		stunned = true;
	}
}
