using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
	public float stunTime = 1;
	public float speed;
	public float lifetime = 3;
	
	void OnCollisionEnter (Collision collision)
	{
		Creature creatureHit = collision.gameObject.GetComponent<Creature> ();
	
		if (creatureHit != null) {
			creatureHit.Stun (stunTime);
		}
		CleanUp ();
	}
    
	void Update ()
	{
		transform.Translate (Vector3.forward * Time.deltaTime * speed);
		if (lifetime <= 0) {
			CleanUp ();
		} else {
			lifetime -= Time.deltaTime;
		}
	}
	
	void CleanUp ()
	{
		Destroy (this.gameObject);
	}
}
