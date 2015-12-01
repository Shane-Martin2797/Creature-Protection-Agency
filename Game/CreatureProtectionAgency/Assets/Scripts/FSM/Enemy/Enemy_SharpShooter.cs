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
    public float waypointSoftEdge = 0f;
    public int currentWaypoint = 0;
    public int previousWaypoint = 0;

    public float timeIdleAtWaypoint = 0f;
    public float idleTimeRemaining = 0f;

    private bool timerReset = false;
    
    public override void Start()
    {
        base.Start();

        if (movementWaypoints.Length <= 0)
        {
            Debug.LogError("There are no waypoints set for the Sharp-Shooter's movement");
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
        if (Vector3.Distance(transform.position, movementWaypoints[currentWaypoint]) <= waypointSoftEdge)
        {
            if ((idleTimeRemaining <= 0) && !timerReset)
            {
                idleTimeRemaining = timeIdleAtWaypoint;
                timerReset = true;
            }
            else
            {
                idleTimeRemaining -= Time.deltaTime;
            }

            if ((idleTimeRemaining <= 0) && timerReset)
            {
                previousWaypoint = currentWaypoint;

                while (currentWaypoint == previousWaypoint)
                {
                    currentWaypoint = Random.Range(0, movementWaypoints.Length);
                }

                timerReset = false;
            }
        }
        else
        {
            navAgent.SetDestination(movementWaypoints[currentWaypoint]);
        }
	}



    //Movement for the other AI types, will (hopefully) work as soon as the scripts are made.
    //
    //
    //public float minX, maxX;
    //public float minZ, maxZ;
    //public Vector3 currentWaypoint;
    //
    //
    //public override void Movement ()
    //{
    //    if (Vector3.Distance(transform.position, currentWaypoint) <= waypointSoftEdge)
    //    {
    //////////////////// If there is no idle time, this code section can be commented out and the change implementation placed exposed.
    //        if ((idleTimeRemaining <= 0) && !timerReset)
    //        {
    //            idleTimeRemaining = timeIdleAtWaypoint;
    //            timerReset = true;
    //        }
    //        else
    //        {
    //            idleTimeRemaining -= Time.deltaTime;
    //        }
    //
    //        if ((idleTimeRemaining <= 0) && timerReset)
    //        {
    //            currentWaypoint = new Vector3 (Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
    //            timerReset = false;
    //        }
    //    }
    //    else
    //    {
    //        navAgent.SetDestination(currentWaypoint);
    //    }
    //}
}
