using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PlayerController : SingletonBehaviour<PlayerController>
{
	public List<Creature> creatureList = new List<Creature> ();

	public Light lightObject;
	
	public Bait baitPrefab;
	public RockController dartPrefab;

	public float baitThrowTimeInterval;

	public float dartThrowInterval;

	DateTime lastBaitThrow;
	DateTime lastDartThrow;

	public float timeForBaitToHitGround;

	public int maxBait = 3;

	int curNumBait = 0;

    public List<EnemyController> enemies;
    private EnemyController closestEnemy;
    public float aimAssistThreshold = 0;

	protected override void OnSingletonAwake ()
	{
		//	Cursor.visible = false;
		Bait.UpdateBait += UpdateBait;

		//make the light point at the mouse position
		PointLight ();
	}
	
    void Start ()
    {
        enemies.Add(FindObjectOfType<Enemy_Chaser>());
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

		if (Input.GetKeyDown (KeyCode.Mouse1) && (lastDartThrow == null || (DateTime.Now - lastDartThrow).Seconds >= dartThrowInterval)) {
			ThrowBait (false);
			
			lastDartThrow = DateTime.Now;
		}

		PointLight ();
	}


	void ThrowBait (bool isBait) 
	{
		ThrowObject throwObject;
		if (isBait) {
			throwObject = Instantiate (baitPrefab, transform.position, Quaternion.identity) as BaitController;
			curNumBait++;
		} 
		else 
		{
			throwObject = Instantiate (dartPrefab, transform.position, Quaternion.identity) as RockController;
		}
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hitInfo;

		Physics.Raycast (ray, out hitInfo);

		if (isBait) 
        {
            throwObject.GetComponent<Rigidbody>().velocity = CalculateTrajectory(transform.position, hitInfo.point);

			throwObject.GetComponent<Rigidbody> ().angularVelocity = Vector3.right * -40.0f * UnityEngine.Random.Range (0.0f, 1.0f);

			throwObject.GetComponent<Rigidbody> ().angularVelocity += Vector3.forward * -40.0f * UnityEngine.Random.Range (0.0f, 1.0f);
		}
		else 
        {
            FindClosestEnemy(hitInfo.point);

            if (Vector3.Distance(hitInfo.point, closestEnemy.transform.position) <= aimAssistThreshold)
            {
                throwObject.GetComponent<RockController>().target = closestEnemy.gameObject;
            }
            else
            {
                throwObject.GetComponent<Rigidbody>().velocity = CalculateTrajectory(transform.position, hitInfo.point);
            }
		}
	}
	
	Vector3 CalculateTrajectory (Vector3 origin, Vector3 destination)
	{
		Vector3 diff = destination - origin;

		float initialVerticalVelocity = (destination.y + -Physics.gravity.y * 0.5f * Mathf.Pow (timeForBaitToHitGround, 2) - origin.y) / timeForBaitToHitGround;

		diff /= timeForBaitToHitGround;

		diff.y = initialVerticalVelocity;

		return diff;
	}

    public float UpdateHUDBait ()
    {
        //(DateTime.Now - lastBaitThrow).Seconds / baitThrowTimeInterval
        return Mathf.Clamp(((DateTime.Now - lastBaitThrow).Seconds / baitThrowTimeInterval) + ((DateTime.Now - lastBaitThrow).Milliseconds / baitThrowTimeInterval)/1000, 0, 1);
    }

    public float UpdateHUDDart()
    {
        //(DateTime.Now - lastBaitThrow).Seconds >= baitThrowTimeInterval
        return Mathf.Clamp(((DateTime.Now - lastDartThrow).Seconds / dartThrowInterval) + ((DateTime.Now - lastDartThrow).Milliseconds / dartThrowInterval)/1000, 0, 1);
    }

    public void FindClosestEnemy (Vector3 _HitPoint)
    {
        int closestEnemyIndex = 0;
        float currentClosestDistance = 9001;

        if (enemies.Count != 0)
        {
            for (int index = 0; index < enemies.Count; index++)
            {
                if (Vector3.Distance(enemies[index].transform.position, _HitPoint) < currentClosestDistance)
                {
                    currentClosestDistance = Vector3.Distance(enemies[index].transform.position, _HitPoint);
                    closestEnemyIndex = index;
                }
            }
            closestEnemy = enemies[closestEnemyIndex];
        }
    }
}
