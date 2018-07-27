using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {


	public enum GAME_STATE {
		GAME_WELCOME,
		GAME_LEVEL_SELECT,
		GAME_GAMEPLAY,
		GAME_RESULTS,
		GAME_STATE_MAX
	}

	UIController uiInstance;

	private GameController(){}
	public GameController instance;
	public  int MAXLEVEL=6;
	private GAME_STATE currentState=GAME_STATE.GAME_STATE_MAX;
	private GAME_STATE previousState=GAME_STATE.GAME_STATE_MAX;

	public GameController GetInstance(){
		if(instance==null)
			instance=new GameController();
		return instance;
	}


	void Start () {
		currentState=GAME_STATE.GAME_WELCOME;
		previousState=GAME_STATE.GAME_STATE_MAX;
	}
	
	void Update () {
		
	}
}
