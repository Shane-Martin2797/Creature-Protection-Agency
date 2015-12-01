using UnityEngine;
using System.Collections;

public class Enemy_Attacking : FSMState<EnemyController>
{
	public float idleFromAttack;
	
	public override void RegisterTransitions ()
	{
		AddTransition<Enemy_Moving> (EnemyEvents.Enemy_State_Idle);
	}
	public override void OnEnter ()
	{
		fsm.context.Attack ();
		context.cooldownTimer = context.cooldownTime;
		fsm.Transition (EnemyEvents.Enemy_State_Idle);
	}
	
	public override void OnExit ()
	{
		context.idleTime = idleFromAttack;
	}
}