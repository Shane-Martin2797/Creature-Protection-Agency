using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class EnemyController : MonoBehaviour
{
	
	public FSM<EnemyController> fsm;
	
	public float range;

	public NavMeshAgent navAgent;

	//Stats of the Hunters
	public float movementSpeed;
	
	//Time spent in idle state before going back into movement
	public float idleTime;
	
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
		
	//Abstract Methods
	public abstract void Attack ();
	public abstract void Movement ();
	
	//How close the poacher has to be to his waypoint before he can re-select a waypoint/go back to idle.
	public float waypointSoftEdge = 0f;
	//Checks if the poacher has selected a position yet.
	public bool gotPos;

	float stun = 0;

	public GameObject stunParticles;
	
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
		if (range == 0) {
			range = visionDistance;
			Debug.Log ("Range for: "
				+ name 
				+ " has not been set, it is now visionDistance");
		}
	}
	
	
	// Use this for initialization
	public virtual void Start ()
	{
		if (stunParticles == null) {
			Debug.LogWarning ("There is no game object set for the stun effect.");
			stunParticles = new GameObject ();
			stunParticles.transform.position = this.transform.position;
			stunParticles.name = "NoAttachedGameObjectForParticleSystem (Enemy)";
		}
	}
	
	/// <summary>
	/// Updates the Instance.
	/// If you override Update, you have base.Update() within the method.
	/// </summary>
	public virtual void Update ()
	{
//		Debug.Log (gameObject.name + " is in " + fsm.currentState.GetType ().Name);

		if (stun > 0) {
			Debug.Log("stunned");
			stun -= Time.deltaTime;
			navAgent.Stop ();
		} else {
			stunParticles.SetActive (false);

			navAgent.Resume ();
			fsm.Update ();
			if (targetCreature == null) {
				Target ();
			} else {
				LookAt (targetCreature.gameObject);
			}
			if (cooldownTimer > 0) {
				cooldownTimer -= Time.deltaTime;
			}
		}
	}
	
	
	public virtual void FixedUpdate ()
	{
		if (stun <= 0) {
			fsm.FixedUpdate ();
		}
	}
	
	
	public virtual void Target ()
	{
		List<Creature> tempList = PlayerController.Instance.creatureList;
		int count = tempList.Count;
		if (count > 0) {
			for (int i = 0; i < count; ++i) {
				if (targetCreature == null) {
					CheckCreature (tempList [i]);
				} 
				if (targetCreature != null) {
					fsm.Transition (EnemyEvents.Enemy_State_Tracking);
					return;
				}
			}
		}
	}

	public virtual void Stun (float _stunTime)
	{
		//run stun logic
		navAgent.Stop (false);  //"Not certain, but this line may be redundant. navAgent.Stop() is used within Update." ~Metalavocado
		stun = _stunTime;

		if(stunParticles != null)
			stunParticles.SetActive (true);
	}

	public virtual void CheckCreature (Creature creature)
	{
		if (creature == null) {
			return;
		}

		float distance = Vector3.Distance (transform.position, creature.transform.position);
		//If the distance is further than we can see then return
		if (distance > visionDistance) {
			//		Debug.Log ("Distance is: " + distance + ". I cannot see that far");
			return;
		}
		
		Vector3 direction = (creature.transform.position - transform.position).normalized;
		//This y direction needs to be cleaned up
		//It needs to be able to ignore y for the dot product and
		//re-use it for the ray cast
		float y_direction = direction.y;
		direction.y = 0;
		
		float dot = Vector3.Dot (transform.forward, direction);
		float cone = Mathf.Cos (visionCone / 2 * Mathf.Deg2Rad);
		
		//If they are not within the cone of vision then return
		if (dot < cone) {
//			Debug.Log ("The dot product is: " + dot + ". My vision cone is: " + cone);
			return;
		}
		direction.y = y_direction;
		
		//Stops the raycast from hitting triggers. When downloaded on windows it may become:
		//Physics2D.raycastHitTriggers = false;
		//Physics.queriesHitTriggers = false;
		
		//Sends out a raycast from where the attack point is, in the direction to the creature.
		RaycastHit hit;
		Physics.Raycast (attackObjectSpawnPoint.position, direction, out hit, visionDistance);
		
		//If we hit nothing with the raycast then break
		if (hit.collider == null) {
//			Debug.Log ("I didn't hit anything with the raycast");
			return;
		}
		//If the gameobject we hit is not equal to the object we are checking for then return.
		if (hit.collider.gameObject != creature.gameObject) {
//			Debug.Log ("I hit something, but it definitely wasn't the target");
			return;
		}
		//Set our target to the creature we are currently checking
		targetCreature = creature;
		
		LookAt (creature.gameObject);
	}
	void LookAt (GameObject gameObj)
	{
//		Debug.Log (gameObj.transform.position);
		Vector3 direction = (gameObj.transform.position - transform.position).normalized;
		
		//This is the angle from the enemy to the targeted creature.
		float angleValue = -(Mathf.Atan2 (direction.z, direction.x) * Mathf.Rad2Deg) + 90;
		
		//This sets our angle to facing the creature.
		transform.localEulerAngles = new Vector3 (0, angleValue, 0);
	}
}
