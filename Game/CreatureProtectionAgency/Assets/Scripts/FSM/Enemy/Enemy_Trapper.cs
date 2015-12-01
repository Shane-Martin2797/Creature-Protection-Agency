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

		cooldownTimer = cooldownTime;
	}

	public override void Attack ()
	{		
		GameObject gameObj = Instantiate (attackObject, attackObjectSpawnPoint.position, transform.rotation) as GameObject;
	}
	Vector3 lastPosition;
	public override void Update ()
	{
		base.Update ();
		Debug.Log ("EnemyTrapperUpdate");
		if (cooldownTimer <= 0) {
			if (!gotPos) {
				fsm.Transition (EnemyEvents.Enemy_State_Tracking);
			}
		} 
		if (lastPosition == transform.position && typeof(Enemy_Idle).GetType().Name == fsm.currentState.GetType().Name) 
		{
			fsm.Transition(EnemyEvents.Enemy_State_Idle);
		}
		lastPosition = transform.position;

	}
	
	public override void Movement ()
	{
		if (!gotPos) {
			currentWaypoint = Vector3.up * 1000;
			NavMeshPath path = new NavMeshPath();

			Vector3 waypointPosition;

			while(currentWaypoint == Vector3.up * 1000)
			{
				waypointPosition = new Vector3 (Random.Range (minX, maxX), transform.position.y, Random.Range (minZ, maxZ));

				if(NavMesh.CalculatePath(transform.position, waypointPosition, NavMesh.AllAreas, path))
				{
					currentWaypoint = waypointPosition;
				}
			}
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