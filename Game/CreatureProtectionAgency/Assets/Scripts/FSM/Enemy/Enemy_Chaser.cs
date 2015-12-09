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
	
	public override void Update ()
	{
		base.Update ();
	}
	
	public override void Movement ()
	{
		
		if (!gotPos) {
			NavMeshPath path = new NavMeshPath ();
			Vector3 point = GetMidPoint (PlayerController.Instance.creatureList);
			int whileLoopBreakIndex = 0;
			while (path.status != NavMeshPathStatus.PathComplete) {
				point += new Vector3 (Random.Range (boundsX.x - (distance / 2), boundsX.y + (distance / 2)), 0, Random.Range (boundsZ.x - distance, boundsZ.y + distance));
				Debug.Log (point);
				NavMesh.CalculatePath (transform.position, point, NavMesh.AllAreas, path);
				whileLoopBreakIndex++;
				if (whileLoopBreakIndex >= 1000) {
					Debug.LogError ("Change the Bounds to a smaller value, it took 1000 iterations and still didn't find a path (CHASER)");
					fsm.Transition (EnemyEvents.Enemy_State_Idle);
					break;
				}
			}
			destination = point;
			gotPos = true;
		}
		destination.y = transform.position.y;

		//Debug.Log (destination);
		if (Vector3.Distance (transform.position, destination) <= waypointSoftEdge) {
			fsm.Transition (EnemyEvents.Enemy_State_Idle);
		} else {
			navAgent.SetDestination (destination);
		}
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
		point /= gameObjectList.Count;
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
