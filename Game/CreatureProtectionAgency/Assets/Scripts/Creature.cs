﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]
public class Creature : MonoBehaviour
{
	public float moveSpeed;

	NavMeshAgent navigator;

	NavMeshPath[] paths;

	int numberOfActiveBaits = -1;

	BaitController target;

	void Awake ()
	{
		navigator = GetComponent<NavMeshAgent> ();
	}

	public void Stun (float stunTime)
	{
		//apply the stun time
	}

	void Update () 
	{
		if (numberOfActiveBaits != PlayerController.activeList.Count) 
		{
			FindClosestPath ();

			numberOfActiveBaits = PlayerController.activeList.Count;
		}

		if (target != null) 
		{
			if((target.transform.position - transform.position).magnitude < 3.0f)
			{
				target.Eat();
			}
		}
	}

	void FindClosestPath ()
	{
		float[] pathLengths = new float[PlayerController.activeList.Count];
		paths = new NavMeshPath[PlayerController.activeList.Count];
		List<float> arrangeablePathLengths = new List<float>();
		for (int index = 0; index < PlayerController.activeList.Count; ++index) 
		{
			arrangeablePathLengths.Add(0);

			if(navigator.SetDestination(PlayerController.activeList[index].transform.position))
			{
				paths[index] = navigator.path;

				float distance = (navigator.path.corners[0] - transform.position).magnitude;

				for(int index2 = 1; index2 < navigator.path.corners.Length; ++index2)
				{
					distance += (navigator.path.corners[index2] - navigator.path.corners[index2 - 1]).magnitude;
				}
				pathLengths[index] = distance;
			}
			else
			{
				pathLengths[index] = float.MaxValue;

				paths[index] = null;
			}
			arrangeablePathLengths[index] = pathLengths[index];
		}

		arrangeablePathLengths.Sort ();

		for (int index = 0; index < paths.Length; ++index) 
		{
			if(pathLengths[index] == arrangeablePathLengths[0])
			{
				navigator.SetDestination(PlayerController.activeList[index].transform.position);
				target = PlayerController.activeList[index];
				break;
			}
		}
	}
}