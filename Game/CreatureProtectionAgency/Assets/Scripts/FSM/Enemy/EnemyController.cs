using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class EnemyController : MonoBehaviour
{
	
	private FSM<EnemyController> fsm;
	
	//Stats of the Hunters
	public float movementSpeed;
	
	//Cooldown time between attacks
	public float cooldownTime;
	public float cooldownTimer;
	
	//For ability to detect the Tasmanian Tigers
	public float visionCone;
	public float visionDistance;
	
	//This is the creature it targets
	public Creature targetCreature;
	
	//Used for the object the player is spawning to attack
	public GameObject attackObject;
	//The point at which this object will be spawned
	public Transform attackObjectSpawnPoint;
	
	//The position the enemy is heading to
	public Vector3 targetPosition;
	
	//Abstract Methods
	public abstract void Attack ();
	public abstract void Movement ();
	
	
	//Virtual Methods
	public virtual void BuildFSM ()
	{
		fsm = new FSM<EnemyController> (this);
		fsm.RegisterState<Enemy_Idle> ();
		fsm.RegisterState<Enemy_Moving> ();
		fsm.RegisterState<Enemy_Tracking> ();
		fsm.RegisterState<Enemy_Attacking> ();
	}
	
	
	public virtual void Awake ()
	{
		BuildFSM ();
	}
	
	
	// Use this for initialization
	public virtual void Start ()
	{
		
	}
	
	/// <summary>
	/// Updates the Instance.
	/// If you override Update, you have base.Update() within the method.
	/// </summary>
	public virtual void Update ()
	{
		fsm.Update ();
		if (targetCreature == null) {
			Target ();
		}
	}
	
	
	public virtual void FixedUpdate ()
	{
		fsm.FixedUpdate ();
	}
	
	
	public virtual void Target ()
	{
		List<Creature> tempList = PlayerController.Instance.creatureList;
		int count = tempList.Count;
		if (count > 0) {
			for (int i = 0; i < count; ++i) {
				if (targetCreature == null) {
					CheckCreature (tempList [i]);
				} else {
					fsm.Transition (EnemyEvents.Enemy_State_Tracking);
					break;
				}
			}
		}
	}
	
	
	public virtual void CheckCreature (Creature creature)
	{
		float distance = Vector3.Distance (transform.position, creature.transform.position);
		//If the distance is further than we can see then return
		if (distance > visionDistance) {
			return;
		}
		
		Vector3 direction = (creature.transform.position - transform.position).normalized;
		
		float dot = Vector3.Dot (transform.up, direction);
		float cone = Mathf.Cos (visionCone / 2 * Mathf.Deg2Rad);
		
		//If they are not within the cone of vision then return
		if (dot < cone) {
			return;
		}
		
		//Stops the raycast from hitting triggers. When downloaded on windows it may become:
		//Physics2D.raycastHitTriggers = false;
		Physics2D.queriesHitTriggers = false;
		
		//Sends out a raycast from where the attack point is, in the direction to the creature.
		RaycastHit2D hit = Physics2D.Raycast (attackObjectSpawnPoint.position, direction, visionDistance);
		
		//If we hit nothing with the raycast then break
		if (hit == null) {
			return;
		}
		//If the gameobject we hit is not equal to the object we are checking for then return.
		if (hit.collider.gameObject != creature.gameObject) {
			return;
		}
		//Set our target to the creature we are currently checking
		targetCreature = creature;
		
		//This is the angle from the enemy to the targeted creature.
		float angleValue = (Mathf.Atan2 (direction.x, direction.y) * Mathf.Rad2Deg) + 180;
		//This sets our angle to facing the creature.
		transform.localEulerAngles = new Vector3 (0, 0, angleValue);
	}
	
}
