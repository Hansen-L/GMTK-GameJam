using UnityEngine;
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

		if (_rb.velocity.magnitude > 0.001) { dashDirection = _rb.velocity; }
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
			_dog.isDashing = false;
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
