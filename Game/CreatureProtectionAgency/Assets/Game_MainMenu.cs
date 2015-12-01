using UnityEngine;
using System.Collections;

class Game_MainMenu : FSMState<GameController>
{
	public override void RegisterTransitions ()
	{
		AddTransition<Game_GameClick> (GameEvents.Main_Menu_Play);
	}

	public override void OnEnter ()
	{
		context.ChangeOverlay (GameController.GameStateEnum.mainMenu);

		Camera.main.gameObject.SetActive (false);
	}

	public override void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Return)) 
		{
			fsm.Transition (GameEvents.Main_Menu_Play);
		}
	}
}