using UnityEngine;
using UnityEngine.Events;

public abstract class Entity : Character
{
	protected IncrementTimer walkBackTimer = new IncrementTimer();
	public UnityEvent OnDie;

	protected bool wasStunt = false;


	protected override void Awake()
	{
		 wasStunt = false;
		RestoreHealth(); // odebrat
		RestoreStamina();
		//walkBackTimer.OnTimerUpdate += new TimerHandler(RestoreStamina);
		//walkBackTimer.OnTimerStart += delegate
		//{
		//	walkTimer.Stop();
		//	runTimer.Stop();
		//	idleTimer.Stop();
		//};
		//walkBackTimer.Init(1, 4, this);

		base.Awake();
	}

	public abstract void Attack();

	public abstract void Defend();

	public virtual void Die()
	{

		Debug.Log("I am dead");
			anim.SetBool("isDead", true);
		

		if (OnDie != null)
		{

			OnDie.Invoke();
		}
	}
	

	public void Jump()
	{

		//+ other jump methods
	}
	protected override void OnDrawGizmos()
	{
		/*if (Weapon != null)
		{
			Gizmos.color = Color.red;
			if(CurrentWeapon!=null)
			Gizmos.DrawWireSphere(transform.position, CurrentWeapon.Range);
		}*/
		base.OnDrawGizmos();
	}
	public void RestoreHealth()
	{
		
		
	}
}
