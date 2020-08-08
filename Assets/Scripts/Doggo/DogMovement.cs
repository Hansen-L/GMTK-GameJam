using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class DogMovement : MonoBehaviour
{

	//public bool dash_jump;
	//public bool more_bork;
	//public bool reverse_bork;
	//public bool bigger_bork;

	//float dash_timer;
	//float stun_timer;
	//float stun_time;

	//Vector2 dash_direction;
	//bool dashing;
	//bool stunned;
	//bool barking;


	//Rigidbody2D body;
	//public ScreenShake screenShake;
	//public GameObject dashJumpCollider;
	//public GameObject barkCollider;
	//public GameObject dashEndEffect;
	//public GameObject barkEffect;

	//public bool isShadow = false;
	//public float shadowDelay = 2f;



	//private Animator animator;
	//private SpriteRenderer dogRenderer;


	//void Start()
	//{
	//	dash_jump = false;
	//}

	//void Update()
	//{
	//	// hacky poop code
	//	animator.SetBool("barking", false);
	//	animator.SetBool("dashing", false);

	//	if (!isShadow) // regular dog
	//	{


	//		if (Input.GetMouseButtonDown(0))
	//		{
	//			barking = true;
	//			Bark();

	//		}
	//	}
	//	else
	//	{
	//		StartCoroutine(ShadowInput());
	//	}


	//	AnimateDog();
	//	barking = false;

	//}

	//public IEnumerator ShadowInput()
	//{
	//	// Gives a value between -1 and 1
	//	float temp_horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
	//	float temp_vertical = Input.GetAxisRaw("Vertical"); // -1 is down

	//	StartCoroutine(ShadowMovement(temp_horizontal, temp_vertical));

	//	if (Input.GetKeyDown("space"))
	//	{
	//		StartCoroutine(ShadowDash());
	//	}

	//	if (Input.GetMouseButtonDown(0))
	//	{
	//		StartCoroutine(ShadowBark());
	//	}

	//	yield return null;
	//}

	//public IEnumerator ShadowMovement(float temp_horizontal, float temp_vertical)
	//{
	//	yield return new WaitForSeconds(shadowDelay);
	//	horizontal = temp_horizontal;
	//	vertical = temp_vertical;
	//}

	//public IEnumerator ShadowBark()
	//{
	//	yield return new WaitForSeconds(shadowDelay);
	//	barking = true;
	//	Bark();
	//}

	//public IEnumerator ShadowDash()
	//{
	//	yield return new WaitForSeconds(shadowDelay);
	//	Dash_start();
	//}

	//void FixedUpdate()
	//{
	//	if (stunned == true)
	//	{
	//		stun_timer += Time.fixedDeltaTime;
	//		body.velocity = new Vector2(0, 0);
	//		if (stun_timer >= stun_time)
	//		{
	//			stunned = false;
	//		}

	//	}
	//	else if (dashing == true)
	//	{

	//		//if (Input.GetMouseButtonDown(0)){
	//		//	dash_attack = true;
	//		//}

	//		dash_timer += Time.fixedDeltaTime;
	//		body.velocity = dash_speed * dash_direction;

	//		//if (dash_timer <= 0.2) {
	//		//	body.velocity = new Vector2(0, 0);
	//		//}

	//		if (dash_timer >= dash_time)
	//		{
	//			Dash_end();
	//		}

	//	}

	//}

	//void AnimateDog()
	//{

	//	if (dashing)
	//	{ animator.SetBool("dashing", true); }
	//	else if (barking)
	//	{ animator.SetBool("barking", true); }
	//}


	//void Dash_start()
	//{
	//	dash_timer = 0;
	//	dashing = true;
	//	dash_direction = body.velocity;
	//	if (dash_direction.magnitude < 0.001)
	//	{
	//		dash_direction = new Vector2(h_velocity, v_velocity);
	//	}
	//	dash_direction.Normalize();
	//	AudioManager.Instance.PlayOneShot("dash");
	//}

	//void Dash_end()
	//{
	//	dashing = false;
	//	body.velocity = new Vector2(0, 0);
	//	stun_time = dash_end_stun;
	//	Stun_start();
	//	if (dash_jump == true)
	//	{
	//		// Spawn collider to push back sheep
	//		StartCoroutine(screenShake.Shake(0.1f, 0.1f));
	//		GameObject dashJumpColliderInstance = Instantiate(dashJumpCollider, this.transform.position, Quaternion.identity);
	//		Destroy(dashJumpColliderInstance, 0.1f);

	//		Vector2 effectPosition = new Vector2(this.transform.position.x, this.transform.position.y + 1.3f);
	//		GameObject dashEndEffectInstance = Instantiate(dashEndEffect, effectPosition, Quaternion.identity);
	//		Destroy(dashEndEffectInstance, 2f);
	//	}
	//}

	//void Bark()
	//{
	//	Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	//	Vector2 mouseDir = new Vector2(mousePos.x - this.transform.position.x, mousePos.y - this.transform.position.y);
	//	mouseDir.Normalize();
	//	Vector2 position = new Vector2(this.transform.position.x, this.transform.position.y) + mouseDir * 2;
	//	GameObject barkInstance = Instantiate(barkCollider, position, Quaternion.identity);
	//	if (more_bork == true)
	//	{
	//		barkInstance.GetComponent<BarkCollider>().velocity = 44;
	//	}
	//	if (reverse_bork == true)
	//	{
	//		barkInstance.GetComponent<BarkCollider>().velocity = -7;
	//	}
	//	if (reverse_bork == true && more_bork == true)
	//	{
	//		barkInstance.GetComponent<BarkCollider>().velocity = -15;
	//	}
	//	if (bigger_bork == true)
	//	{
	//		barkInstance.GetComponent<BarkCollider>().transform.localScale = new Vector2(15, 15);
	//	}
	//	barkInstance.GetComponent<BarkCollider>().getPos(this.transform.position);
	//	Destroy(barkInstance, 0.1f);

	//	float angleToCamera = Mathf.Atan2(mouseDir.x, mouseDir.y) * Mathf.Rad2Deg - 90f;

	//	// Spawn bark effect
	//	float offsetX = 0.3f;
	//	float offsetY = 0.12f;
	//	Vector2 barkEffectPosition;
	//	if (body.velocity.x < 0) //moving left, offset spawn position of bark effect
	//	{
	//		barkEffectPosition = new Vector2(this.transform.position.x - offsetX, this.transform.position.y + offsetY);
	//		GameObject barkEffectInstance = Instantiate(barkEffect, barkEffectPosition, new Quaternion(0f, 0f, 0f, 1));
	//		barkEffectInstance.transform.parent = gameObject.transform;
	//		barkEffectInstance.transform.rotation = Quaternion.Euler(0f, 0f, -angleToCamera + 180f); // Don't ask me I have no clue...
	//		Destroy(barkEffectInstance, 2f);
	//	}
	//	else
	//	{
	//		barkEffectPosition = new Vector2(this.transform.position.x + offsetX, this.transform.position.y + offsetY);
	//		GameObject barkEffectInstance = Instantiate(barkEffect, barkEffectPosition, new Quaternion(0f, 180f, 0f, 1));
	//		barkEffectInstance.transform.parent = gameObject.transform;
	//		barkEffectInstance.transform.rotation = Quaternion.Euler(0f, 0f, -angleToCamera + 180f);
	//		Destroy(barkEffectInstance, 2f);
	//	}
	//	AudioManager.Instance.PlayOneShot("bark", true, true);
	//}

	//void Stun_start()
	//{
	//	stun_timer = 0;
	//	stunned = true;
	//}
}
