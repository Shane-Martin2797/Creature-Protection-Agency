using UnityEngine;
using System.Collections;

public class Trap : BaitController
{
	public float stunTime;
	
	void OnCollisionEnter (Collision col)
	{
		if (!hitGround && col.gameObject.tag == "Ground")
		{
			hitGround = true;
			
			PlayerController.activeList.Add (this);
			
			Destroy (GetComponent<Rigidbody> ());
			
			transform.eulerAngles = Vector3.up * -90;
		}
	}
	
	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == "Creature")
		{
			Activate (col.GetComponent<Creature> ());
		}
	}

	void Activate (Creature creature)
	{
		creature.Stun (stunTime);

		Destroy (this.gameObject);
	}	
}
