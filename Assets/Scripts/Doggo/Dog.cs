using UnityEngine;
using System;
using Utils;

public class Dog : MonoBehaviour 
{
    #region Constants
    public const float acceleration = 1f;
    public const float maxSpeed = 7f;
    public const float dashSpeed = 25f;
    public const float dashTime = 0.35f;
    public const float stunTime = 0.2f;
    public const float friction = 0.4f;
	#endregion

	public GameObject barkCollider;
	public GameObject barkEffect;

    public float xInput = 0f;
    public float yInput = 0f;
	public float prevxInput = 1f;
    public float prevyInput = 1f;

	public bool isDashing = false;
	public bool isStunned = false;
	public bool isBarking = false;

    private StateMachine _stateMachine;
	private Animator _animator;
	private SpriteRenderer _spriteRenderer;
	private Rigidbody2D _rb;

	private float xBoundary;
	private float yBoundary;
	private int baseLayer;

	private float xVelocity;
	private float yVelocity;

	private void Start()
	{
		#region Configuring State Machine

		_animator = GetComponent<Animator>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_rb = GetComponent<Rigidbody2D>();

		_stateMachine = new StateMachine();

		// Instantiating states
		var running = new Running(this, _animator);
		var idle = new Idle(this, _animator);
		var dashing = new Dashing(this, _animator, _rb);
		var stunned = new Stunned(this, _rb);

		// Assigning transitions
		_stateMachine.AddAnyTransition(stunned, IsStunned());
		At(running, idle, IsIdle());
		At(idle, running, IsMoving());
		At(idle, dashing, IsDashing());
		At(running, dashing, IsDashing());
		At(stunned, idle, IsNotStunned());

		// Starting state
		_stateMachine.SetState(running);

		// Method to assign transitions easily
		void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

		// Transition conditions
		Func<bool> IsMoving() => () => (xInput != 0 || yInput != 0);
		Func<bool> IsIdle() => () => (xInput == 0 && yInput == 0);
		Func<bool> IsDashing() => () => (isDashing);
		Func<bool> IsNotDashing() => () => (!isDashing);
		Func<bool> IsStunned() => () => (isStunned);
		Func<bool> IsNotStunned() => () => (!isStunned);
		#endregion

		#region Instantiating instance variables
		// Get boundaries object to set player movement boundaries
		GameObject boundaryObj = GameObject.Find("Boundaries");
		Boundaries boundary = boundaryObj.GetComponent<Boundaries>();
		xBoundary = boundary.playerBoundary_x;
		yBoundary = boundary.playerBoundary_y;

		// Base sorting layer
		baseLayer = GetComponent<SpriteRenderer>().sortingOrder;
		#endregion
	}

	private void Update()
    {
        ProcessInput(); // Read player input
		ClampPosition(); // Prevent player going out of bounds
		Utils.Utils.SetRenderLayer(gameObject, baseLayer);
		FlipSprite();

		_stateMachine.Tick();
    }

	private void FixedUpdate()
	{
        _stateMachine.FixedTick();
	}


	#region Methods called every frame regardless of state

	public void ProcessInput()
    {
        // Gives a value between -1 and 1
        xInput = Input.GetAxisRaw("Horizontal"); // -1 is left
        yInput = Input.GetAxisRaw("Vertical"); // -1 is down
		// Store previous inputs
		if (xInput != 0 || yInput != 0) {
			prevxInput = xInput;
			prevyInput = yInput;
		}

		if (Input.GetKeyDown("space")) { isDashing = true; }
		if (Input.GetMouseButtonDown(0)) { Bark(); }
	}

	public void ClampPosition()
	{
		Vector2 position;
		position.x = Mathf.Clamp(this.transform.position.x, -xBoundary, xBoundary);
		position.y = Mathf.Clamp(this.transform.position.y, -yBoundary, yBoundary);
		this.transform.position = position;
	}

	public void FlipSprite()
	{
		if (_rb.velocity.x > 0) // moving right
			{ _spriteRenderer.flipX = true; }
		else if (_rb.velocity.x < 0) // moving left
			{ _spriteRenderer.flipX = false; }
	}

	#endregion


	#region Methods called from states

	public void ProcessMovement() // Called from running and idle states
	{
		if (xInput * xVelocity <= 0)
		{
			xVelocity *= Dog.friction;
		}
		if (yInput * yVelocity <= 0)
		{
			yVelocity *= Dog.friction;
		}
		xVelocity = Mathf.Clamp(xVelocity + xInput * Dog.acceleration, -Dog.maxSpeed, Dog.maxSpeed);
		yVelocity = Mathf.Clamp(yVelocity + yInput * Dog.acceleration, -Dog.maxSpeed, Dog.maxSpeed);

		Vector2 total_velocity = new Vector2(xVelocity, yVelocity);

		_rb.velocity = Vector2.ClampMagnitude(total_velocity, Dog.maxSpeed);
	}

	public void Bark()
	{
		_animator.SetTrigger("barkingTrig");
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 mouseDir = new Vector2(mousePos.x - _rb.transform.position.x, mousePos.y - _rb.position.y);
		mouseDir.Normalize();

		Vector2 barkPos = new Vector2(_rb.transform.position.x, _rb.transform.position.y) + mouseDir * 2;
		GameObject barkInstance = Instantiate(barkCollider, barkPos, Quaternion.identity);

		barkInstance.GetComponent<BarkCollider>().getPos(this.transform.position);
		Destroy(barkInstance, 0.1f);

		float angleToCamera = Mathf.Atan2(mouseDir.x, mouseDir.y) * Mathf.Rad2Deg - 90f;

		// Spawn bark effect
		float offsetX = 0.3f;
		float offsetY = 0.12f;
		Vector2 barkEffectPosition;
		if (_rb.velocity.x < 0) //moving left, offset spawn position of bark effect
		{
			barkEffectPosition = new Vector2(_rb.transform.position.x - offsetX, _rb.transform.position.y + offsetY);
			GameObject barkEffectInstance = Instantiate(barkEffect, barkEffectPosition, new Quaternion(0f, 0f, 0f, 1));
			barkEffectInstance.transform.parent = gameObject.transform;
			barkEffectInstance.transform.rotation = Quaternion.Euler(0f, 0f, -angleToCamera + 180f); // Don't ask me I have no clue...
			Destroy(barkEffectInstance, 2f);
		}
		else
		{
			barkEffectPosition = new Vector2(_rb.transform.position.x + offsetX, _rb.transform.position.y + offsetY);
			GameObject barkEffectInstance = Instantiate(barkEffect, barkEffectPosition, new Quaternion(0f, 180f, 0f, 1));
			barkEffectInstance.transform.parent = gameObject.transform;
			barkEffectInstance.transform.rotation = Quaternion.Euler(0f, 0f, -angleToCamera + 180f);
			Destroy(barkEffectInstance, 2f);
		}
		AudioManager.Instance.PlayOneShot("bark", true, true);
	}

	#endregion
}
