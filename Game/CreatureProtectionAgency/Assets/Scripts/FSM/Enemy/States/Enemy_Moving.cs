using UnityEngine;
using System.Collections;

public class Enemy_Moving : FSMState<EnemyController>
{
	public override void RegisterTransitions ()
	{
		AddTransition<Enemy_Idle> (EnemyEvents.Enemy_State_Idle);
		AddTransition<Enemy_Tracking> (EnemyEvents.Enemy_State_Tracking);
	} 
	
	public override void Update ()
	{
		base.Update ();
		fsm.context.Movement ();
        //if (fsm.context.targetPosition == null) 
        //{
        //    fsm.Transition (EnemyEvents.Enemy_State_Idle);
        //}
	}

}
