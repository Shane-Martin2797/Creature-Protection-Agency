using UnityEngine;
using System.Collections;

public class RockController : ThrowObject
{
	public float stunTime = 3.0f;

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Poacher") 
		{
			col.gameObject.GetComponent<EnemyController>().Stun(stunTime);
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
