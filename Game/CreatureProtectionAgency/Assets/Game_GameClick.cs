using UnityEngine;
using System.Collections;

class Game_GameClick : FSMState<GameController>
{
	public override void RegisterTransitions ()
	{
		AddTransition<Game_Game> (GameEvents.Begin_Play);
	}
	
	public override void OnEnter ()
	{
		context.ChangeOverlay (GameController.GameStateEnum.gameClick);

		Camera.main.gameObject.SetActive (true);
	}

	public override void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Mouse0)) 
		{
			fsm.Transition (GameEvents.Begin_Play);
		}
	}
}

