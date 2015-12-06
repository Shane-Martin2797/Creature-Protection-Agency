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
	public override void Update ()
	{
		base.Update ();
	}

	public override void Attack ()
	{		
		GameObject bullet = Instantiate (attackObject, attackObjectSpawnPoint.position, transform.rotation) as GameObject;
		Vector3 diff = targetCreature.transform.position - attackObjectSpawnPoint.position; //This is the Vector from the spawn of the bullet to the enemy.
		//The Y is already taken account for with it instantiating at my rotation. The Z just offsets the bullet by turning it. So only X needs to be changed.

		//looks in the direction of the target
		transform.LookAt(bullet.transform.position);
		
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
		Vector3 targetPos = new Vector3 (movementWaypoints [currentWaypoint].x, transform.position.y, movementWaypoints [currentWaypoint].z);
		if (Vector3.Distance (transform.position, targetPos) <= waypointSoftEdge) {
			fsm.Transition (EnemyEvents.Enemy_State_Idle);
		} else {
			navAgent.SetDestination (targetPos);
		}
	}
}
