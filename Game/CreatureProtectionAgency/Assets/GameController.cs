using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : SingletonBehaviour<GameController> 
{
	[System.Serializable]
	public class GameStateUI
	{
		public RectTransform mainMenu, gameClick, game, gameOver;
	}

	public enum GameStateEnum
	{
		mainMenu, 
		gameClick, 
		game, 
		gameOver
	}
	
	FSM<GameController> fsm;

	public GameStateUI overlays;

	void Start () 
	{
		Time.timeScale = 0;
		BuildFSM ();

		ChangeOverlay (GameStateEnum.gameClick);
	}
	[HideInInspector]
	public float timer = 300.0f;

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Mouse0)) 
		{
			ChangeOverlay(GameStateEnum.game);
			Time.timeScale = 1;
		}
		timer -= Time.unscaledDeltaTime;
		if (timer < 0 || PlayerController.Instance.creatureList.Count == 0) 
		{
			ChangeOverlay(GameStateEnum.gameOver);
			//Camera.main.gameObject.SetActive(false);

			if(Input.GetKeyDown(KeyCode.Backspace))
			{
				Application.LoadLevel(Scenes.logo);
			}
		}
	}

	void BuildFSM ()
	{
		fsm = new FSM<GameController> (this);
		fsm.RegisterState<Game_MainMenu> (true);
		fsm.RegisterState<Game_GameClick> ();
		fsm.RegisterState<Game_Game> ();
		fsm.RegisterState<Game_GameOver> ();

/*		fsm.RegisterTransition(GameEvents.Main_Menu_Play);
		fsm.RegisterTransition(GameEvents.Begin_Play);
		fsm.RegisterTransition(GameEvents.End_The_Game);
		fsm.RegisterTransition(GameEvents.Restart);
*/	}
	
	public void ChangeOverlay (GameStateEnum enumVal) 
	{
		overlays.mainMenu.gameObject.SetActive (false);
		overlays.gameClick.gameObject.SetActive (false);
		overlays.game.gameObject.SetActive (false);
		overlays.gameOver.gameObject.SetActive (false);

		switch (enumVal) 
		{
		case GameStateEnum.mainMenu :
			overlays.mainMenu.gameObject.SetActive (true);
			break;
		case GameStateEnum.gameClick :
			overlays.gameClick.gameObject.SetActive (true);
			break;
		case GameStateEnum.game :
			overlays.game.gameObject.SetActive (true);
			break;
		case GameStateEnum.gameOver :
			overlays.gameOver.gameObject.SetActive (true);
			break;

		}
	}
}
