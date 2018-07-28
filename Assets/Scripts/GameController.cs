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

	public static GameController instance;
	public  int MAXLEVEL=6;
	[SerializeField]
	private GAME_STATE currentState=GAME_STATE.GAME_STATE_MAX;
	[SerializeField]
	private GAME_STATE previousState=GAME_STATE.GAME_STATE_MAX;
	public int gameTouchCount;
	[SerializeField]
	private int gameMainScore;

	private GameController(){}

	public static GameController GetInstance(){
		return instance;
	}
		
	public void AddScore(int _score)
	{
		gameMainScore+=_score;
	}

	public void ReduceScore(int _score)
	{
		gameMainScore-=_score;
	}

	public int GetScore(){
		return gameMainScore;
	}

	public void  ChangeGameState(GAME_STATE _state){
		previousState=currentState;
		currentState=_state;
		UIController.GetInstance().OnStateChange();
	}

		//OnStateChange();
//	void OnStateChange(){
//		switch(currentState){
//		case GAME_STATE.GAME_GAMEPLAY:
//			{
//				
//			}
//			break;
//		case GAME_STATE.GAME_LEVEL_SELECT:
//			{
//				
//			}
//			break;
//		case GAME_STATE.GAME_RESULTS:
//			{
//				
//			}
//			break;
//		}
//	}
//

	public GAME_STATE GetGameState(){
		return currentState;
	}

	void Start () {
		PlayerPrefs.SetInt("score",0);//for test purpose
		if(instance) {
			GameObject.Destroy(gameObject);
			Debug.LogError("Already initialised");
		} else {
			GameController.instance = this;
		}

		if (PlayerPrefs.GetInt("score",0)<0)
			PlayerPrefs.SetInt("score",0);
		
		gameMainScore=PlayerPrefs.GetInt("score");

		currentState=GAME_STATE.GAME_WELCOME;
		previousState=GAME_STATE.GAME_STATE_MAX;


	}

}
