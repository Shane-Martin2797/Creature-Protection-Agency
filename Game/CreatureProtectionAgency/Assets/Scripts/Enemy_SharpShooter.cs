using UnityEngine;
using System.Collections;

public class Enemy_SharpShooter : EnemyController
{
	//This is the percent chance out of 100, for whether bullet dispersion will occur
	public float accuracy;
	//This is the maximum degrees of bullet dispersion. E.g. 15 is -15 to 15 degrees.
	public float maxBulletDispersion;
	
	private float deltaPercent = 100;
	
	public override void Attack ()
	{		
		GameObject bullet = Instantiate (attackObject, attackObjectSpawnPoint, transform.rotation);
		Vector3 rot = bullet.transform.localEulerAngles;
		if (Random.value <= (accuracy / deltaPercent)) {
			rot.z += Random.Range (-maxBulletDispersion, maxBulletDispersion + 1);
		}
		bullet.transform.localEulerAngles = rot;
		
		cooldownTimer = cooldownTime;
	}

	public override void Movement ()
	{
		
	}
}
