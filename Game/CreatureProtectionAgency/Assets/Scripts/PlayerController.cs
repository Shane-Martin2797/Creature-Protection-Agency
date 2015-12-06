using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PlayerController : SingletonBehaviour<PlayerController>
{
	public static List<BaitController> activeList;
	public List<Creature> creatureList = new List<Creature> ();

	public Light lightObject;
	
	public Bait baitPrefab;
	public RockController rockPrefab;

	public float baitThrowTimeInterval;

	public float rockThrowInterval;

	DateTime lastBaitThrow;
	DateTime lastRockThrow;

	public float timeForBaitToHitGround;

	public int maxBait = 3;

	int curNumBait = 0;
	
	protected override void OnSingletonAwake ()
	{
		//	Cursor.visible = false;
		Bait.UpdateBait += UpdateBait;
		activeList = new List<BaitController> ();

		//make the light point at the mouse position
		PointLight ();
	}
	
	public float lightAimSpeed;
	Vector3 lightAimPosition;
	
	void PointLight ()
	{
		//lerp between the last position and this position
		lightAimPosition = lightObject.transform.position + Camera.main.ScreenPointToRay(Input.mousePosition).direction;
		

		Vector3 curAim = Vector3.Lerp (lightObject.transform.position + lightObject.transform.forward, lightAimPosition, lightAimSpeed/(lightAimPosition - transform.forward).magnitude);
		lightObject.transform.LookAt(curAim);

	}

	void UpdateBait ()
	{
		curNumBait--;
	}

	void Update ()
	{
		if (curNumBait < maxBait && Input.GetKeyDown (KeyCode.Mouse0) && (lastBaitThrow == null || (DateTime.Now - lastBaitThrow).Seconds >= baitThrowTimeInterval)) {
			ThrowBait (true);

			lastBaitThrow = DateTime.Now;
		}

		if (Input.GetKeyDown (KeyCode.Mouse1) && (lastRockThrow == null || (DateTime.Now - lastRockThrow).Seconds >= rockThrowInterval)) {
			ThrowBait (false);
			
			lastRockThrow = DateTime.Now;
		}

		PointLight ();
	}


	void ThrowBait (bool isBait) 
	{
		ThrowObject throwObject;
		if(isBait)
			throwObject = Instantiate (baitPrefab, transform.position, Quaternion.identity) as BaitController;
		else
			throwObject = Instantiate (rockPrefab, transform.position, Quaternion.identity) as RockController;

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hitInfo;

		Physics.Raycast (ray, out hitInfo);

		throwObject.GetComponent<Rigidbody> ().velocity = CalculateTrajectory (transform.position, hitInfo.point);

		throwObject.GetComponent<Rigidbody> ().angularVelocity = Vector3.right * -40.0f * UnityEngine.Random.Range(0.0f, 1.0f);

		throwObject.GetComponent<Rigidbody> ().angularVelocity += Vector3.forward * -40.0f * UnityEngine.Random.Range(0.0f, 1.0f);

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
