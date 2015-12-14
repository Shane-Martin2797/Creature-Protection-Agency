using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bait : BaitController 
{
	/// <summary>
	/// The amount of points this bait has remaining that will be reduced when eaten
	/// </summary>
	public float foodPoints;
	float initialFoodPoints;

    float lifeTime = 3;
    float initialLifeTime;

	Material _material;

	Color initialColour;

    public Transform lifeTimeSlider;

    float sliderSize;

	void Start ()
	{
		initialFoodPoints = foodPoints;

		_material = GetComponent<Renderer> ().material;

		initialColour = _material.color;

        initialLifeTime = lifeTime;

        lifeTimeSlider.transform.SetParent(null);

        sliderSize = lifeTimeSlider.transform.localScale.x;
	}

	void Update () 
	{
        lifeTime -= Time.deltaTime;
        lifeTimeSlider.position = transform.position + Vector3.up * 0.85f;
        lifeTimeSlider.rotation = Quaternion.identity;
        lifeTimeSlider.localScale = new Vector3(lifeTime * sliderSize / initialLifeTime, lifeTimeSlider.localScale.y, lifeTimeSlider.localScale.z);
     
        if (transform.position.y < -1 || lifeTime < 0) 
		{
			Destroy(this.gameObject);
		}
	}

    
    protected override void ChildClassDestroy ()
    {
        if (lifeTimeSlider != null)
        {
            Destroy(lifeTimeSlider.gameObject);
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
