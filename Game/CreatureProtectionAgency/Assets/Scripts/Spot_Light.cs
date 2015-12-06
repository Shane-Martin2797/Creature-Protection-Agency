using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class Spot_Light : MonoBehaviour
{
	public EnemyController enemy;
	private Light light;
	
	void Awake ()
	{
		light = this.GetComponent<Light> ();
		if (enemy.visionCone == 360) {
			light.type = LightType.Point;
			
		} else {
			light.type = LightType.Spot;
			light.spotAngle = enemy.visionCone;
		}
		light.range = enemy.visionDistance;
	}
}
