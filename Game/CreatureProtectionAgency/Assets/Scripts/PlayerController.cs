using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PlayerController : SingletonBehaviour<PlayerController> 
{
	public static List<BaitController> activeList;
	
	public Bait baitPrefab;

	public float baitThrowTimeInterval;

	DateTime lastThrow;

	public float timeForBaitToHitGround;

	public int maxBait = 5;

	int curNumBait = 0;

	protected override void OnSingletonAwake ()
	{
	//	Cursor.visible = false;
		Bait.UpdateBait += UpdateBait;
		activeList = new List<BaitController> ();
    }

	void UpdateBait ()
	{
		curNumBait--;
	}

	void Update () 
	{
		if (curNumBait < maxBait && Input.GetKeyDown (KeyCode.Mouse0) && (lastThrow == null || (DateTime.Now - lastThrow).Seconds > baitThrowTimeInterval)) 
		{
			ThrowBait();

			lastThrow = DateTime.Now;
		}
	}

	void ThrowBait () 
	{
		Bait bait = Instantiate (baitPrefab, transform.position, Quaternion.identity) as Bait;

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hitInfo;

		Physics.Raycast (ray, out hitInfo);

		bait.GetComponent<Rigidbody> ().velocity = CalculateTrajectory (transform.position, hitInfo.point);

		curNumBait++;
	}
	
	Vector3 CalculateTrajectory (Vector3 origin, Vector3 destination)
	{
		Vector3 diff = destination - origin;

		float initialVerticalVelocity = (destination.y + -Physics.gravity.y * 0.5f * Mathf.Pow (timeForBaitToHitGround, 2) - origin.y) / timeForBaitToHitGround;

		diff /= timeForBaitToHitGround;

		diff.y = initialVerticalVelocity;

		return diff;
	}
}
