using UnityEngine;
using System.Collections;

public class Trap : BaitController 
{
	public float stunTime;

	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == "Creature") {
			Activate (col.GetComponent<Creature> ());
		}
	}

	void Activate (Creature creature) 
	{
		creature.Stun (stunTime);

		Destroy (this.gameObject);
	}	
}
