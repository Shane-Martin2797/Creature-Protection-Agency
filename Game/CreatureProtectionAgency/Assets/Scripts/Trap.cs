using UnityEngine;
using System.Collections;

public class Animal : MonoBehaviour
{
	public void Stun (float stunTime)
	{
		//apply the stun time
	}
}

public class Trap : MonoBehaviour 
{
	public float stunTime;

	void OnTriggerEnter (Collider col)
	{
		Activate (col.GetComponent<Animal>());
	}

	void Activate (Animal animal) 
	{
		animal.Stun (stunTime);
	}
}
