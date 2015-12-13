using UnityEngine;
using System.Collections;

public class Enemy_Trapper : EnemyController
{
	public Vector3 currentWaypoint = Vector3.zero;
	Vector3 lastPosition;

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

	public override void Update ()
	{
		base.Update ();
		if (cooldownTimer <= 0)
		{
			cooldownTimer = (cooldownTime / 4);
			idleTime = 2f;
			if (transform.position != lastPosition)
			{
				lastPosition = transform.position;
				Attack ();
				cooldownTimer = cooldownTime;
				fsm.Transition (EnemyEvents.Enemy_State_Idle);
			}
		} 
	}
	
	public override void Movement ()
	{
		if (!gotPos)
		{
			currentWaypoint = Vector3.up * 1000;
			NavMeshPath path = new NavMeshPath ();

			Vector3 waypointPosition;

			while (currentWaypoint == Vector3.up * 1000)
			{
				waypointPosition = new Vector3 (Random.Range (minX, maxX), transform.position.y, Random.Range (minZ, maxZ));

				int whileLoopBreakIndex = 0;
				
				if (path.status != NavMeshPathStatus.PathComplete)
				{
					currentWaypoint = waypointPosition;
					NavMesh.CalculatePath (transform.position, waypointPosition, NavMesh.AllAreas, path);
					whileLoopBreakIndex++;
					if (whileLoopBreakIndex >= 1000)
					{
						Debug.LogError ("Change the min x and min y to a smaller value, it took 1000 iterations and still didn't find a path (TRAPPER)");
						fsm.Transition (EnemyEvents.Enemy_State_Idle);
						break;
					}
				}
			}
			gotPos = true;
		}
		
		currentWaypoint.y = transform.position.y;
		
		if (Vector3.Distance (transform.position, currentWaypoint) <= waypointSoftEdge)
		{
			fsm.Transition (EnemyEvents.Enemy_State_Idle);
		}
		else
		{
			navAgent.SetDestination (currentWaypoint);
		}
	}
}