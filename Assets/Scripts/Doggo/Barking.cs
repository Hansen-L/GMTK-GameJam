using UnityEngine;
using System.Threading;

public class Barking : IState
{
	private Animator _animator;
	private Dog _dog;
	private Rigidbody2D _rb;

	public Barking(Dog dog, Animator animator)
	{
		_dog = dog;
		_animator = animator;
	}

	public void OnEnter()
	{
		_animator.SetBool("barking", true);
		_dog.Bark();
		_dog.isBarking = false;
	}

	public void Tick()
	{
	}

	public void FixedTick()
	{
	}


	public void OnExit()
	{
		Debug.Log("Not Barking");
		_animator.SetBool("barking", false);
	}
}
