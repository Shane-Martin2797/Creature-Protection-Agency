using UnityEngine;
using System.Collections;

public class TitleController : MonoBehaviour {
	public GameObject OptionsScreen;
	// Use this for initialization
	void Start () {
		OptionsScreen.SetActive (false);
	
	}
	public void OnClick_Play(){
		Application.LoadLevel (Scenes.StartingCinematic);
	}
	public void OnClick_Options(){
		OptionsScreen.SetActive (true);
	}
	public void OnClick_Back(){
		OptionsScreen.SetActive (false);
	}
	public void OnClick_Quit(){
		Application.Quit ();
	}
}
