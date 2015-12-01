using UnityEngine;
using System.Collections;

public class Enemy_Trapper : EnemyController
{
	public float waypointSoftEdge = 0f;
	public Vector3 currentWaypoint = 0;

	public float timeIdleAtWaypoint = 0f;
	public float idleTimeRemaining = 0f;
	
	public float minX, maxX;
	public float minZ, maxZ;

	private bool timerReset = false;
    
	public override void Start ()
	{
		base.Start ();
	}

	public override void Attack ()
	{		
	}	
	
	
	public override void Movement ()
	{
		if (Vector3.Distance (transform.position, currentWaypoint) <= waypointSoftEdge) {

			if ((idleTimeRemaining <= 0) && !timerReset) {
				idleTimeRemaining = timeIdleAtWaypoint;
				timerReset = true;
			} else {
				idleTimeRemaining -= Time.deltaTime;
			}
	
			if ((idleTimeRemaining <= 0) && timerReset) {
				currentWaypoint = new Vector3 (Random.Range (minX, maxX), 0, Random.Range (minZ, maxZ));
				timerReset = false;
			}
		} else {
			navAgent.SetDestination (currentWaypoint);
		}
	}
}
