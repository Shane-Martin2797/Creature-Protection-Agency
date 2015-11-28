using UnityEngine;
using System.Collections;

public class Bait : BaitController 
{
	public delegate void BaitEaten ();
	
	public static event BaitEaten UpdateBait;	

	void Update () 
	{
		if (transform.position.y < -1) 
		{
			Destroy(this.gameObject);
		}
	}
	
	void OnDestroy () 
	{
		UpdateBait ();
	}
}
