using UnityEngine;
using System.Collections;

public class Enemy_Idle : FSMState<EnemyController>
{
	float delay;
	public float delayDefault = 3;
	public float percentChance = 50;
	
	public override void RegisterTransitions ()
	{
		AddTransition<Enemy_Moving> (EnemyEvents.Enemy_State_Moving);
		AddTransition<Enemy_Tracking> (EnemyEvents.Enemy_State_Tracking);
	}
	public override void OnEnter ()
	{
		if (fsm.context.idleTime <= 0) {
			delay = delayDefault;
		} else {
			delay = fsm.context.idleTime;
			fsm.context.idleTime = 0;
		}
	}
	public override void Update ()
	{
		base.Update ();

		if (delay <= 0) {
			delay = delayDefault;
			if (Random.value <= (percentChance / 100)) {
				fsm.Transition (EnemyEvents.Enemy_State_Moving);
			}
		} else {
			delay -= Time.deltaTime;
		}
//		Debug.Log ("Update Idle");
	}
}
