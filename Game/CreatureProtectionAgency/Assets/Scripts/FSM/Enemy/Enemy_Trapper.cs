using UnityEngine;
using System.Collections;

public class Enemy_Trapper : EnemyController
{
	public Vector3 currentWaypoint = Vector3.zero;

	public float minX, maxX;
	public float minZ, maxZ;
    
	public override void Start ()
	{
		base.Start ();
	}

	public override void Attack ()
	{		
		GameObject gameObj = Instantiate (attackObject, attackObjectSpawnPoint.position, transform.rotation) as GameObject;
	}
	
	public override void Update ()
	{
		base.Update ();
		if (cooldownTimer <= 0) {
			if (!gotPos) {
				fsm.Transition (EnemyEvents.Enemy_State_Tracking);
			}
		} 
	}
	
	public override void Movement ()
	{
		if (!gotPos) {
			currentWaypoint = new Vector3 (Random.Range (minX, maxX), transform.position.y, Random.Range (minZ, maxZ));
			gotPos = true;
		}
		
		currentWaypoint.y = transform.position.y;
		
		if (Vector3.Distance (transform.position, currentWaypoint) <= waypointSoftEdge) {
			fsm.Transition (EnemyEvents.Enemy_State_Idle);
		} else {
			navAgent.SetDestination (currentWaypoint);
		}
	}
}