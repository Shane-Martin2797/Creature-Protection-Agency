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
	
	public override void Update ()
	{
		base.Update ();

		if (delay <= 0) {
			delay = delayDefault;
			if (Random.value <= (percentChance / 100)) {
				PickTargetPosition ();
			}
		} else {
			delay -= Time.deltaTime;
		}
	}

	///<summary>
	/// This method should choose a position from a list of waypoints for the enemy to travel to.
	/// </summary>
	void PickTargetPosition ()
	{
		Vector3 pickedPos = Vector3.zero;
		fsm.context.targetPosition = pickedPos;
		fsm.Transition (EnemyEvents.Enemy_State_Moving);
	}
}
