using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public abstract class BaitController : MonoBehaviour
{
	bool hitGround = false;

	void Awake ()
	{
		if (PlayerController.activeList == null) 
		{
			PlayerController.activeList = new List<BaitController>();
		}
	}

	public virtual void Eat ()
	{

	}

	void OnCollisionEnter (Collision col) 
	{
		if (!hitGround && col.gameObject.tag == "Ground") 
		{
			hitGround = true;

			PlayerController.activeList.Add(this);
		}
	}

	protected void RemoveFromList ()
	{
		PlayerController.activeList.Remove (this);
	}
}