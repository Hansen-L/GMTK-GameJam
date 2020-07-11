﻿using System.Collections;
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

	Vector3 characterScale;

	public float acceleration;
	public float max_speed;
	public float friction;
	public float dash_speed;
	public float dash_time;
	public float dash_end_stun;
	public ScreenShake screenShake;
	public GameObject dashJumpCollider;
	public GameObject barkCollider;

	private float boundary_x;
	private float boundary_y;


	void Start ()
	{
	   	body = GetComponent<Rigidbody2D>();
	   	characterScale = transform.localScale;

		GameObject boundaryObj = GameObject.Find("Boundaries");
		BoundaryNumbers boundary = boundaryObj.GetComponent<BoundaryNumbers>();

		boundary_x = boundary.playerBoundary_x;
		boundary_y = boundary.playerBoundary_y;
	}

	void Update()
	{
	   	// Gives a value between -1 and 1
	   	horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
	   	vertical = Input.GetAxisRaw("Vertical"); // -1 is down

		if (horizontal != 0){
			transform.localScale = new Vector3(characterScale.x*-horizontal, characterScale.y, characterScale.z);
		}

	   	if (Input.GetKeyDown("space")){
	   		Dash_start();
	   	}

	   	if (Input.GetMouseButtonDown(0)){
	   		Bark();
	   	}

		Vector2 position;

		position.x = Mathf.Clamp(this.transform.position.x, -boundary_x, boundary_x);
		position.y = Mathf.Clamp(this.transform.position.y, -boundary_y, boundary_y);
		this.transform.position = position;
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

			if (dash_timer <= 0.2){
				body.velocity = new Vector2(0,0);
			}

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
		}

	}

	void Dash_start()
	{
		dash_timer = 0;
		dashing = true;
		dash_direction =  body.velocity;
		dash_direction.Normalize();
	}

	void Dash_end()
	{
		dashing = false;
		body.velocity = new Vector2(0,0);
		stun_time = dash_end_stun;
		Stun_start();
		StartCoroutine(screenShake.Shake(0.1f, 0.1f));

		// Spawn collider to push back sheep
		GameObject dashJumpColliderInstance = Instantiate(dashJumpCollider, this.transform.position, Quaternion.identity);
		Destroy(dashJumpColliderInstance, 0.1f);
	}

	void Bark(){
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 direction = new Vector2 (mousePos.x - this.transform.position.x, mousePos.y - this.transform.position.y);
		direction.Normalize();
		Vector2 position =  new Vector2(this.transform.position.x,this.transform.position.y)  + direction * 2;
		GameObject barkInstance = Instantiate(barkCollider, position, Quaternion.identity);
		barkInstance.GetComponent<BarkCollider>().getPos(this.transform.position);
		Destroy(barkInstance, 0.1f);
	}

	void Stun_start()
	{
		stun_timer = 0;
		stunned = true;
	}
}
