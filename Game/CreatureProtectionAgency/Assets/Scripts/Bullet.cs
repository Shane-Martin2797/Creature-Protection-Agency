using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
	public float stunTime = 1;
	public float speed;
	public float lifetime = 3;

    void Start()
    {
        Destroy(this.gameObject, lifetime);
    }

	void OnCollisionEnter (Collision collision)
	{
		Creature creatureHit = collision.gameObject.GetComponent<Creature> ();
	
		if (creatureHit != null) {
			creatureHit.Stun (stunTime);
		}

        Destroy(this.gameObject);
	}
    
	void Update ()
	{
		transform.Translate (Vector3.forward * Time.deltaTime * speed);
	}
}
