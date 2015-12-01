using UnityEngine;
using System.Collections;

public class Bait : BaitController 
{
	/// <summary>
	/// The amount of points this bait has remaining that will be reduced when eaten
	/// </summary>
	public float foodPoints;
	float initialFoodPoints;

	Material _material;

	Color initialColour;

	void Start ()
	{
		initialFoodPoints = foodPoints;

		_material = GetComponent<Renderer> ().material;

		initialColour = _material.color;
	}

	void Update () 
	{
		if (transform.position.y < -1) 
		{
			Destroy(this.gameObject);
		}
	}

	float colourMag = 1.0f;

	public override void Eat () {
		foodPoints -= Time.deltaTime;


		_material.color = initialColour * foodPoints / initialFoodPoints;

		if (foodPoints <= 0) 
		{
			Destroy(this.gameObject);
		}
	}
}
