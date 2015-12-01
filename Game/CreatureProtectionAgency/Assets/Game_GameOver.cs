using UnityEngine;
using System.Collections;

class Game_GameOver : FSMState<GameController>
{
	public override void RegisterTransitions ()
	{
		AddTransition<Game_GameClick> (GameEvents.Restart);
	}
	
	public override void OnEnter ()
	{
		context.ChangeOverlay (GameController.GameStateEnum.gameOver);
	}

	public override void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Backspace)) 
		{
			fsm.Transition (GameEvents.End_The_Game);
		}
	}
}

