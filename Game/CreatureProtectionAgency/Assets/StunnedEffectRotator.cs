using UnityEngine;
using System.Collections;

public class StunnedEffectRotator : MonoBehaviour {
    public float rotationSpeed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    transform.eulerAngles += new Vector3(0,rotationSpeed,0);
	}
}
