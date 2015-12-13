using UnityEngine;
using System.Collections;

public class RockController : ThrowObject
{
	public float stunTime = 3.0f;
    public GameObject target;

    private bool hasTarget;
    public float moveVelocity = 10;

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
	void Start () 
    {
		transform.Rotate (50, 180, 180);

        if (target != null)
        {
            hasTarget = true;
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (hasTarget)
        {
            transform.LookAt(target.transform);
            GetComponent<Rigidbody>().velocity = transform.forward * moveVelocity;
        }
	}
}
