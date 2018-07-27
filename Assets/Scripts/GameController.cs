using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public enum GAME_STATE {
		GAME_WELCOME
		,GAME_LEVEL_SELECT
		,GAME_GAMEPLAY
		,Game_PAUSE
		,GAME_RESULTS
		,GAME_STATE_MAX
	}

	UIController uiInstance;

	private GameController(){}
	public static GameController instance;
	public  int MAXLEVEL=6;
	private GAME_STATE currentState=GAME_STATE.GAME_STATE_MAX;
	private GAME_STATE previousState=GAME_STATE.GAME_STATE_MAX;
	public int touchCount;
	private int Main_Score;

	public static GameController GetInstance(){
		return instance;
	}
		
	public void AddScore(int _score)
	{
		Main_Score+=_score;
	}
	public void ReduceScore(int _score)
	{
		Main_Score-=_score;
	}
	public int GetScore(){
		return Main_Score;
	}

	public void  changeGameState(GAME_STATE _state){
		
	}
	void Start () {
		if (PlayerPrefs.GetInt("score",-1)==-1)
			PlayerPrefs.SetInt("score",0);
		Main_Score=PlayerPrefs.GetInt("score");

		currentState=GAME_STATE.GAME_WELCOME;
		previousState=GAME_STATE.GAME_STATE_MAX;

		if(instance) {
			GameObject.Destroy(gameObject);
			Debug.LogError("Already initialised");
		} else {
			GameController.instance = this;
		}
	}

}
