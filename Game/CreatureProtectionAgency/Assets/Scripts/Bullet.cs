using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour 
{
    public float stunTime = 1;
	
    void OnCollisionEnter (Collision collision)
    {
        Creature creatureHit = collision.gameObject.GetComponent<Creature>();

        if (creatureHit != null)
        {
            creatureHit.Stun (stunTime);
        }
    }
}
