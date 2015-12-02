using UnityEngine;
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

	float stunTime;

	Renderer _renderer;

	Color originalColor;

	Color stunColor;
	
	void Start ()
	{
		navigator = GetComponent<NavMeshAgent> ();
		PlayerController.Instance.creatureList.Add (this);
		_renderer = GetComponent<Renderer> ();
		stunColor = Color.red;
		originalColor = _renderer.material.color;
	}

	public void Stun (float _stunTime)
	{
		//apply the stun time
		stunTime = _stunTime;
	}

	void Update () 
	{
		if (stunTime < 0) {
			_renderer.material.color = originalColor;
			if (numberOfActiveBaits != PlayerController.activeList.Count) {
				FindClosestPath ();

				numberOfActiveBaits = PlayerController.activeList.Count;
			}

			if (target != null) {
				navigator.SetDestination(target.transform.position);
				
				if ((target.transform.position - transform.position).magnitude < 3.0f) {
					target.Eat ();
				}
			}
		} else 
		{
			_renderer.material.color = stunColor;

			stunTime -= Time.deltaTime;
		}
	}

	void FindClosestPath ()
	{
		float[] pathLengths = new float[PlayerController.activeList.Count];
		paths = new NavMeshPath[PlayerController.activeList.Count];
		List<float> arrangeablePathLengths = new List<float>();

		for (int index = 0; index < PlayerController.activeList.Count; ++index) 
		{
			paths[index] = new NavMeshPath();
			if(!NavMesh.CalculatePath(transform.position, PlayerController.activeList[index].transform.position, NavMesh.AllAreas, paths[index]))
			{
				paths[index] = null;
			}

			arrangeablePathLengths.Add(0);

			if(paths[index] != null)
			{
				float distance = 0;

				for(int index2 = 1; index2 < paths[index].corners.Length; ++index2)
				{
					distance += (paths[index].corners[index2] - paths[index].corners[index2 - 1]).magnitude;
				}
				pathLengths[index] = distance;
			}
			else
			{
				pathLengths[index] = float.MaxValue;
			}
			arrangeablePathLengths[index] = pathLengths[index];
		}

		arrangeablePathLengths.Sort ();

		for (int index = 0; index < paths.Length; ++index) 
		{
			if(pathLengths[index] == arrangeablePathLengths[0])
			{
				target = PlayerController.activeList[index];
				break;
			}
		}
	}
}