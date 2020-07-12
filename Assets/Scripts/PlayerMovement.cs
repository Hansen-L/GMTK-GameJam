using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

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
	bool barking;
	bool dash_attack = false;
	public bool dash_jump; 
	public bool more_bork;
	public bool reverse_bork;

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
	public GameObject dashEndEffect;
	public GameObject barkEffect;

	private float boundary_x;
	private float boundary_y;
	private int baseLayer;

	private Animator animator;
	private SpriteRenderer dogRenderer;

    private GameObject audioManagerObj;
    private AudioManager audioManager;


    void Start()
	{
		body = GetComponent<Rigidbody2D>();
		characterScale = transform.localScale;

		GameObject boundaryObj = GameObject.Find("Boundaries");
		BoundaryNumbers boundary = boundaryObj.GetComponent<BoundaryNumbers>();

		boundary_x = boundary.playerBoundary_x;
		boundary_y = boundary.playerBoundary_y;

		animator = this.GetComponent<Animator>();
		dogRenderer = this.GetComponent<SpriteRenderer>();
		baseLayer = this.GetComponent<SpriteRenderer>().sortingOrder;

        audioManagerObj = GameObject.Find("Audio Manager");
        audioManager = audioManagerObj.GetComponent<AudioManager>();

        dash_jump = false;
    }

	void Update()
	{
		// hacky poop code
		animator.SetBool("barking", false);
		animator.SetBool("dashing", false);

		// Gives a value between -1 and 1
		horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
		vertical = Input.GetAxisRaw("Vertical"); // -1 is down

		if (Input.GetKeyDown("space")) {
			Dash_start();
		}

		if (Input.GetMouseButtonDown(0)) {
			barking = true;
			Bark();
			
		}

		Vector2 position;

		position.x = Mathf.Clamp(this.transform.position.x, -boundary_x, boundary_x);
		position.y = Mathf.Clamp(this.transform.position.y, -boundary_y, boundary_y);
		this.transform.position = position;

		AnimateDog();
		barking = false;

		Utils.Utils.SetRenderLayer(gameObject, baseLayer);
	}
	void FixedUpdate()
	{
		if (stunned == true) {
			stun_timer += Time.fixedDeltaTime;
			body.velocity = new Vector2(0, 0);
			if (stun_timer >= stun_time) {
				stunned = false;
			}

		} else if (dashing == true) {

			if (Input.GetMouseButtonDown(0)){
				dash_attack = true;
			}

			dash_timer += Time.fixedDeltaTime;
			body.velocity = dash_speed * dash_direction;

			//if (dash_timer <= 0.2) {
			//	body.velocity = new Vector2(0, 0);
			//}

			if (dash_timer >= dash_time) {
				Dash_end();
			}

		} else {
			dash_attack = false;
			if (horizontal * h_velocity <= 0) {
				h_velocity *= friction;
			}
			if (vertical * v_velocity <= 0) {
				v_velocity *= friction;
			}

			h_velocity = Mathf.Clamp(h_velocity + horizontal * acceleration, -max_speed, max_speed);
			v_velocity = Mathf.Clamp(v_velocity + vertical * acceleration, -max_speed, max_speed);

			Vector2 total_velocity = new Vector2(h_velocity, v_velocity);

			body.velocity = Vector2.ClampMagnitude(total_velocity, max_speed);
		}

	}

	void AnimateDog()
	{
        // If player is moving
        if (horizontal != 0 || vertical != 0)
        {
            if (!animator.GetBool("isMoving"))
            {
                audioManager.Play("run");
            }
            animator.SetBool("isMoving", true);
        }
		else {
            if (animator.GetBool("isMoving"))
            {
                audioManager.Stop("run");
            }
            animator.SetBool("isMoving", false);
        }

		if (dashing)
		{ animator.SetBool("dashing", true); }
		else if (barking) 
		{ animator.SetBool("barking", true);}
		else if (body.velocity.x > 0) // moving right
		{ dogRenderer.flipX = true; }
		else if (body.velocity.x < 0) // moving left
		{ dogRenderer.flipX = false; }
	}


	void Dash_start()
	{
		dash_timer = 0;
		dashing = true;
		dash_direction =  body.velocity;
		if (dash_direction.magnitude < 0.001){
			dash_direction = new Vector2(h_velocity, v_velocity);
		}
		dash_direction.Normalize();
        audioManager.PlayOneShot("dash");
    }

	void Dash_end()
	{
		dashing = false;
		body.velocity = new Vector2(0,0);
		stun_time = dash_end_stun;
		Stun_start();
		if (dash_jump == true){ 
			// Spawn collider to push back sheep
			StartCoroutine(screenShake.Shake(0.1f, 0.1f));
			GameObject dashJumpColliderInstance = Instantiate(dashJumpCollider, this.transform.position, Quaternion.identity);
			Destroy(dashJumpColliderInstance, 0.1f);

			Vector2 effectPosition = new Vector2(this.transform.position.x, this.transform.position.y + 1.3f);
			GameObject dashEndEffectInstance = Instantiate(dashEndEffect, effectPosition, Quaternion.identity);
			Destroy(dashEndEffectInstance, 2f);
		}
	}

	void Bark(){
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 mouseDir = new Vector2 (mousePos.x - this.transform.position.x, mousePos.y - this.transform.position.y);
		mouseDir.Normalize();
		Vector2 position =  new Vector2(this.transform.position.x,this.transform.position.y)  + mouseDir * 2;
		GameObject barkInstance = Instantiate(barkCollider, position, Quaternion.identity);
		if (more_bork == true){
			barkInstance.GetComponent<BarkCollider>().velocity = 40;
		}
		if (reverse_bork == true){
			barkInstance.GetComponent<BarkCollider>().velocity = -20;
		}
		barkInstance.GetComponent<BarkCollider>().getPos(this.transform.position);
		Destroy(barkInstance, 0.1f);

		float angleToCamera = Mathf.Atan2(mouseDir.x, mouseDir.y) * Mathf.Rad2Deg - 90f;

		// Spawn bark effect
		float offsetX = 0.3f;
		float offsetY = 0.12f;
		Vector2 barkEffectPosition;
		if (body.velocity.x < 0) //moving left, offset spawn position of bark effect
		{
			barkEffectPosition = new Vector2(this.transform.position.x - offsetX, this.transform.position.y + offsetY);
			GameObject barkEffectInstance = Instantiate(barkEffect, barkEffectPosition, new Quaternion(0f, 0f, 0f, 1));
			barkEffectInstance.transform.parent = gameObject.transform;
			barkEffectInstance.transform.rotation = Quaternion.Euler(0f, 0f, -angleToCamera + 180f); // Don't ask me I have no clue...
			Destroy(barkEffectInstance, 2f);
		}
		else
		{
			barkEffectPosition = new Vector2(this.transform.position.x + offsetX, this.transform.position.y + offsetY);
			GameObject barkEffectInstance = Instantiate(barkEffect, barkEffectPosition, new Quaternion(0f, 180f, 0f, 1));
			barkEffectInstance.transform.parent = gameObject.transform;
			barkEffectInstance.transform.rotation = Quaternion.Euler(0f, 0f, -angleToCamera + 180f);
			Destroy(barkEffectInstance, 2f);
        }
        audioManager.PlayOneShot("bark");
    }

	void Stun_start()
	{
		stun_timer = 0;
		stunned = true;
	}
}
