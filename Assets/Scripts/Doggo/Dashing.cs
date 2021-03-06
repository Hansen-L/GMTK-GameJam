﻿using UnityEngine;
using System.Threading;

public class Dashing : IState
{
	private Animator _animator;
	private Dog _dog;
	private Rigidbody2D _rb;

	private float dashTimer;
	private Vector2 dashDirection;

	public Dashing(Dog dog, Animator animator, Rigidbody2D rb)
	{
		_dog = dog;
		_animator = animator;
		_rb = rb;
	}

	public void OnEnter()
	{
		_animator.SetBool("dashing", true);

		dashTimer = 0f;

		if ( _dog.xInput != 0 || _dog.yInput != 0) { dashDirection = new Vector2(_dog.xInput, _dog.yInput); }
		else { dashDirection = new Vector2(_dog.prevxInput, _dog.prevyInput); } // If we aren't holding a direction, dash in last direction

		dashDirection.Normalize();
		AudioManager.Instance.PlayOneShot("dash");
	}

	public void Tick()
	{
		dashTimer += Time.fixedDeltaTime;
		_rb.velocity = Dog.dashSpeed * dashDirection;


		if (dashTimer >= Dog.dashTime)
		{
			Debug.Log(dashTimer);
			_dog.isDashing = false;
			_dog.isStunned = true; // Stun at the end of dash
		}
	}

	public void FixedTick()
	{
	}


	public void OnExit()
	{
		_animator.SetBool("dashing", false);
	}
}
