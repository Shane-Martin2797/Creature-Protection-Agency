using UnityEngine;
using System.Collections;

public class BringInHUD : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        Application.LoadLevelAdditive(Scenes.HUD);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
