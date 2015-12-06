using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy_Chaser : EnemyController
{
	public float idleTimeDef = .1f;
	public Vector2 boundsX = new Vector2 (-1, 1);
	public Vector2 boundsZ = new Vector2 (-1, 1);
	Vector3 destination;
	
	public override void Awake ()
	{
		base.Awake ();
	}
	public override void Attack ()
	{
		Chaser_Attack attack = attackObject.GetComponent<Chaser_Attack> ();
		if (attack != null) {
			attack.target = GetClosestCreature (PlayerController.Instance.creatureList);
		}
		attackObject.SetActive (true);
		idleTime = idleTimeDef;
	}

	public override void Movement ()
	{
		if (!gotPos) {
			Vector3 point = GetMidPoint (PlayerController.Instance.creatureList);
			point += new Vector3 (Random.Range (boundsX.x - distance, boundsX.y + distance), 0, Random.Range (boundsZ.x - distance, boundsZ.y + distance));
			destination = point;
			gotPos = true;
		}
		navAgent.SetDestination (destination);
		
//		if (PlayerController.Instance.creatureList.Count > 0) {
//			Creature creature = GetClosestCreature (PlayerController.Instance.creatureList);
//			if (creature != null) {
//				navAgent.SetDestination (creature.transform.position);
//			}
//		}
	}
	
	
	float distance;
	Vector3 GetMidPoint (List<Creature> gameObjectList)
	{
		Vector3 point = Vector3.zero;
		float maxDistance = 0;
		
		
		for (int i = 0; i < gameObjectList.Count; ++i) {
			point += gameObjectList [i].transform.position;
			for (int j = 0; j < gameObjectList.Count; ++j) {
				float dist = Vector3.Distance (gameObjectList [i].transform.position, gameObjectList [j].transform.position);
				if (dist > maxDistance) {
					maxDistance = dist;
				}	
			}
		}
		distance = maxDistance;
		
		return point;
	}
	
	Creature GetClosestCreature (List<Creature> creatures)
	{
		if (creatures.Count == 0) {
			return null;
		}
		float closestDist = float.MaxValue;
		int closestCreatureIndex = 0;
		for (int i = 0; i < creatures.Count; ++i) {
			if (creatures [i] == null) {
				continue;
			}
			float currDist = Vector3.Distance (transform.position, creatures [i].transform.position);
			if (currDist < closestDist) {
				closestDist = currDist;
				closestCreatureIndex = i;
			}
		}
				
		//Set our target to the creature we are currently checking
		return creatures [closestCreatureIndex];
	}

}
