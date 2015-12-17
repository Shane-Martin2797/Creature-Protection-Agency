using UnityEngine;
using System.Collections;

public class Chaser_Attack : MonoBehaviour
{
	public float lifetimeOfAttack = .2f;
	float timer;
	public Creature target;
    public GameObject cagePrefab;
	
	void OnTriggerEnter (Collider col)
	{
		Notify (col.gameObject);
	}
	
	void Notify (GameObject gameObj)
	{
		Creature creature = gameObj.GetComponent<Creature> ();
		if (creature != null) {
			//Kills the creature
			if (target != null) {
				if (creature == target) {
					Destroy (gameObj.GetComponent<Creature>());
                    Destroy(gameObj.GetComponent<NavMeshAgent>());
                    gameObj.GetComponent<Rigidbody>().isKinematic = true;
                    Instantiate(cagePrefab, gameObj.transform.position, gameObj.transform.rotation);
					target = null;
				}
			}
		}
	}
	
	void Update ()
	{
		if (timer <= 0) {
			this.gameObject.SetActive (false);
		} else {
			timer -= Time.deltaTime;
		}
	}
	
	void OnEnable ()
	{
		timer = lifetimeOfAttack;	
	}
}
