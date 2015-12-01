using UnityEngine;
using System.Collections;

public class Enemy_Tracking : FSMState<EnemyController>
{
	public float idleFromTracking;
	
	public override void RegisterTransitions ()
	{		
		AddTransition<Enemy_Idle> (EnemyEvents.Enemy_State_Idle);
		AddTransition<Enemy_Attacking> (EnemyEvents.Enemy_State_Attacking);
	}
	
	
	public override void Update ()
	{
		base.Update ();
		Track ();
		//If the poacher loses sight, count down until he has 'lost' the creature
		//and return to the idle state for a few seconds.
		if (false) {
			context.idleTime = idleFromTracking;
			fsm.Transition (EnemyEvents.Enemy_State_Idle);
		}
	}
	
	void Track ()
	{
		if (context.cooldownTimer <= 0) {
			fsm.Transition (EnemyEvents.Enemy_State_Attacking);
		} else {
			//Follow the creature it is tracking
			context.navAgent.SetDestination (context.targetCreature.transform.position);
		}
	}
	
	
}
