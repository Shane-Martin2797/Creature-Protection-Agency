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
		Chaser_Attack attack = attackObject.GetComponent<Chaser_Attack> ();
		if (attack != null) {
			attack.target = GetClosestCreature (PlayerController.Instance.creatureList);
		}
		attackObject.SetActive (true);
		idleTime = idleTimeDef;
	}

	public override void Movement ()
	{
		if (PlayerController.Instance.creatureList.Count > 0) {
			Creature creature = GetClosestCreature (PlayerController.Instance.creatureList);
			if(creature != null) {
				navAgent.SetDestination (creature.transform.position);
			}
		}
	}
	
	Creature GetClosestCreature (List<Creature> creatures)
	{
		if (creatures.Count == 0) {
			return null;
		}
		float closestDist = float.MaxValue;
		int closestCreatureIndex = 0;
		for (int i = 0; i < creatures.Count; ++i) {
			if(creatures[i] == null)
			{
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
