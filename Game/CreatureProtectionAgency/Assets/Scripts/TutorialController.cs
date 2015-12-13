using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class TutorialController : MonoBehaviour {
	public float tutorialTimer;

	// Use this for initialization
	void Start () {
		tutorialTimer = 15;
	}
	
	// Update is called once per frame
	void Update () {
		tutorialTimer -= Time.deltaTime;
		if (tutorialTimer <= 0) {
			Application.LoadLevel (Scenes.StartingCinematic);
		}
	}
}
