using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy_Chaser : EnemyController
{
	public float idleTimeDef = .1f;
	
	public override void Awake ()
	{
		base.Awake ();
	}
	public override void Attack ()
	{
		attackObject.SetActive (true);
		idleTime = idleTimeDef;
	}

	public override void Movement ()
	{
		if (PlayerController.Instance.creatureList.Count > 0) {
			navAgent.SetDestination (GetClosestCreature (PlayerController.Instance.creatureList).transform.position);
		}
	}
	
	Creature GetClosestCreature (List<Creature> creatures)
	{
		float closestDist = float.MaxValue;
		int closestCreatureIndex = 0;
		for (int i = 0; i < creatures.Count; ++i) {
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
