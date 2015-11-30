using UnityEngine;
using System.Collections;

public class Enemy_Tracking : FSMState<EnemyController>
{

	public override void RegisterTransitions ()
	{		
		AddTransition<Enemy_Idle> (EnemyEvents.Enemy_State_Idle);
		AddTransition<Enemy_Attacking> (EnemyEvents.Enemy_State_Attacking);
	}
	
	
	public override void Update ()
	{
		base.Update ();
		Track ();
		if (false) {
			fsm.Transition (EnemyEvents.Enemy_State_Idle);
		}
	}
	
	void Track ()
	{
		if (false) {
			fsm.Transition (EnemyEvents.Enemy_State_Attacking);
		}
	}
}
