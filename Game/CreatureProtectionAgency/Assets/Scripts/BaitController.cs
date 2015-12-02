using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public abstract class BaitController : MonoBehaviour
{
	public delegate void BaitEaten ();
	
	public static event BaitEaten UpdateBait;	


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

			Destroy(GetComponent<Rigidbody>());

			transform.eulerAngles = Vector3.forward * -90;
		}
	}

	void OnDestroy () 
	{
		UpdateBait ();
		
		RemoveFromList ();
	}

	protected void RemoveFromList ()
	{
		PlayerController.activeList.Remove (this);
	}
}