using UnityEngine;
using System.Collections;

class Game_Game : FSMState<GameController>
{
	//5 minute timer
	float timer = 300.0f;
	public override void RegisterTransitions ()
	{
		AddTransition<Game_GameOver> (GameEvents.End_The_Game);
	}
	
	public override void OnEnter ()
	{
		context.ChangeOverlay (GameController.GameStateEnum.game);
	}
	
	public override void Update ()
	{
		timer -= Time.deltaTime;

		if (timer < 0.0f) 
		{
			fsm.Transition (GameEvents.End_The_Game);
		}
	}
}

