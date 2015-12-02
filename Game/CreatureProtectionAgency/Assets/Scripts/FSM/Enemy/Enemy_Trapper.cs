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
	
	
	public override void Movement ()
	{
		if (!gotPos) {
			currentWaypoint = new Vector3 (Random.Range (minX, maxX), 0, Random.Range (minZ, maxZ));
			gotPos = true;
		}
		
		if (Vector3.Distance (transform.position, currentWaypoint) <= waypointSoftEdge) {
			fsm.Transition (EnemyEvents.Enemy_State_Idle);
		} else {
			navAgent.SetDestination (new Vector3 (currentWaypoint.x, transform.position.y, currentWaypoint));
		}
	}
}
