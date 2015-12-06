using UnityEngine;
using System.Collections;

public class RockController : ThrowObject
{
	public float stunTime = 3.0f;

	void OnCollisionEnter(Collision col)
	{
		EnemyController enemy = col.collider.GetComponent<EnemyController> ();
		if (enemy != null) 
		{
			Debug.Log (true);
			enemy.Stun(stunTime);
		}
		Destroy (this.gameObject);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
