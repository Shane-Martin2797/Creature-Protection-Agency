using UnityEngine;
using System.Collections;

public class TitleController : MonoBehaviour {
	public GameObject OptionsScreen;
	public GameObject CreditsScreen;
	public GameObject ControlsScreen;
	public GameObject titleObject;
	// Use this for initialization
	void Start () {
		OptionsScreen.SetActive (false);
	
	}
	public void OnClick_Play(){
		Application.LoadLevel (Scenes.StartingCinematic);
	}
	public void OnClick_Options(){
		OptionsScreen.SetActive (true);
		titleObject.SetActive (false);
		CreditsScreen.SetActive (false);
		ControlsScreen.SetActive (false);

	}
	public void OnClick_Credits(){
		CreditsScreen.SetActive (true);
		titleObject.SetActive (false);
		OptionsScreen.SetActive (false);
		ControlsScreen.SetActive (false);
	}
	public void OnClick_Controls(){
		ControlsScreen.SetActive (true);
		titleObject.SetActive (false);
		OptionsScreen.SetActive (false);
		CreditsScreen.SetActive (false);
	}
	public void OnClick_Back(){
		OptionsScreen.SetActive (false);
		CreditsScreen.SetActive (false);
		ControlsScreen.SetActive (false);
		titleObject.SetActive (true);
	}
	public void OnClick_Quit(){
		Application.Quit ();
	}
}
