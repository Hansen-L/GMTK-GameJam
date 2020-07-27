using UnityEngine;
using System.Threading;

public class Running : IState 
{
    private Animator _animator;
    private Dog _dog;

    public Running(Dog dog, Animator animator)
    {
        _dog = dog;
        _animator = animator;
    }

    public void OnEnter() 
	{
		AudioManager.Instance.Play("run");
		_animator.SetBool("isMoving", true);
	}

    public void Tick() 
    {
	}

    public void FixedTick()
    {
		_dog.ProcessMovement();
    }


    public void OnExit() 
	{
		AudioManager.Instance.Stop("run");
		_animator.SetBool("isMoving", false);
	}
}
