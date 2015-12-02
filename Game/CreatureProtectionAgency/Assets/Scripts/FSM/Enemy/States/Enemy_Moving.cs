using UnityEngine;
using System.Collections;

public class Enemy_Moving : FSMState<EnemyController>
{

	public float idleTimeFromMoving;

	public override void RegisterTransitions ()
	{
		AddTransition<Enemy_Idle> (EnemyEvents.Enemy_State_Idle);
		AddTransition<Enemy_Tracking> (EnemyEvents.Enemy_State_Tracking);
	} 
	public override void OnEnter ()
	{
		context.gotPos = false;
	}
	public override void Update ()
	{
		base.Update ();
		fsm.context.Movement ();
	}
	
	public override void OnExit ()
	{
		fsm.context.idleTime = idleTimeFromMoving;
	}
}
