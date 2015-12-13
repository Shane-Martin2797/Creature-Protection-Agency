using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public abstract class BaitController : ThrowObject
{
	public delegate void BaitEaten ();

    public SphereCollider collider;

    public float radiusOfEffect = 15;

    float sizeMultiplier;
	
	public static event BaitEaten UpdateBait;	

	bool hitGround = false;

    List<Creature> creatureList;

	void Awake ()
	{
        creatureList = new List<Creature>();
        collider = gameObject.AddComponent<SphereCollider>();
        collider.isTrigger = true;
        sizeMultiplier = 1;
        if(GetComponent<Trap>() != null)
        {
            sizeMultiplier = 0.5f;
        }
        UpdateAOE();
	}

	public virtual void Eat ()
	{

	}

    protected void UpdateAOE ()
    {
        collider.radius = sizeMultiplier * radiusOfEffect / transform.localScale.magnitude;

        //update the visual
    }

	void OnCollisionEnter (Collision col) 
	{
		if (!hitGround && col.gameObject.tag == "Ground") 
		{
			hitGround = true;

			Destroy(GetComponent<Rigidbody>());

			transform.eulerAngles = Vector3.forward * -90;
		}
	}

    void OnTriggerEnter(Collider col)
    {
        Creature creature = col.gameObject.GetComponent<Creature>();
        if (creature != null)
        {
            creatureList.Add(creature);

            creature.AddBait(this);
        }
    }

    void OnTriggerExit(Collider col)
    {
        Creature creature = col.gameObject.GetComponent<Creature>();
        if (creature != null)
        {
            creatureList.Remove(creature);

            creature.RemoveBait(this);
        }
    }

    public void CreatureDeath (Creature instance)
    {
           creatureList.Remove(instance);
    }

	void OnDestroy () 
	{
		UpdateBait ();

        foreach (Creature creature in creatureList)
        {
            if(creature != null)
                creature.RemoveBait(this);
        }
	}
}