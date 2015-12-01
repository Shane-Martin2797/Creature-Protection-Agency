using UnityEngine;
using System.Collections;

public class Enemy_SharpShooter : EnemyController
{
	//This is the percent chance out of 100, for whether bullet dispersion will occur
	public float accuracy;
	//This is the maximum degrees of bullet dispersion. E.g. 15 is -15 to 15 degrees.
	public float maxBulletDispersion;
	
	private float deltaPercent = 100;

	public Vector3[] movementWaypoints;
	public int currentWaypoint = 0;
	public int previousWaypoint = 0;
	
    
	public override void Start ()
	{
		base.Start ();

		if (movementWaypoints.Length <= 0) {
			Debug.LogError ("There are no waypoints set for the Sharp-Shooter's movement");
		}
	}

	public override void Attack ()
	{		
		GameObject bullet = Instantiate (attackObject, attackObjectSpawnPoint.position, transform.rotation) as GameObject;
		Vector3 rot = bullet.transform.localEulerAngles;
		if (Random.value <= (accuracy / deltaPercent)) {
			rot.z += Random.Range (-maxBulletDispersion, maxBulletDispersion + 1);
		}
		bullet.transform.localEulerAngles = rot;
		
		cooldownTimer = cooldownTime;
	}

	public override void Movement ()
	{
		if (!gotPos) {
			previousWaypoint = currentWaypoint;
		
			while (currentWaypoint == previousWaypoint) {
				currentWaypoint = Random.Range (0, movementWaypoints.Length);
			}
			gotPos = true;
		}
	
		if (Vector3.Distance (transform.position, movementWaypoints [currentWaypoint]) <= waypointSoftEdge) {
			fsm.Transition (EnemyEvents.Enemy_State_Idle);
		} else {
			navAgent.SetDestination (movementWaypoints [currentWaypoint]);
		}
	}
}
