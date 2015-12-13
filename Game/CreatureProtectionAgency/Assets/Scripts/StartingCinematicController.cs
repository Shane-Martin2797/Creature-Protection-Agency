using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class StartingCinematicController : MonoBehaviour {
	public float Timer = 40;
	public void Start(){
		Timer = 40;
	}
	public void Update(){
		Timer -= Time.deltaTime;
		if (Timer <= 0) {
			Application.LoadLevel (Scenes.Tutorial);
		}
	}

}
