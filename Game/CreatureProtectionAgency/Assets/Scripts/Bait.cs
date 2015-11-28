using UnityEngine;
using System.Collections;

public class Bait : BaitController 
{
	public delegate void BaitEaten ();
	
	public static event BaitEaten UpdateBait;	

	/// <summary>
	/// The amount of points this bait has remaining that will be reduced when eaten
	/// </summary>
	public int foodPoints;

	void Update () 
	{
		if (transform.position.y < -1) 
		{
			Destroy(this.gameObject);
		}
	}

	void Eat () {
		foodPoints--;

		if (foodPoints <= 0) 
		{
			Destroy(this.gameObject);
		}
	}
	
	void OnDestroy () 
	{
		UpdateBait ();
	}
}
