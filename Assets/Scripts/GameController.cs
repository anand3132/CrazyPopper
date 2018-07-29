using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class controls diffrent game states in the game
public class GameController : MonoBehaviour {
    public enum GAME_STATE {
        GAME_WELCOME
        , GAME_LEVEL_SELECT
        , GAME_GAMEPLAY
        , Game_PAUSE
        , GAME_RESULTS
        , GAME_STATE_MAX
    }

    public static GameController instance;
    [SerializeField]
    private GAME_STATE currentState = GAME_STATE.GAME_STATE_MAX;

	// for undo. Not yet implemented.
    [SerializeField]
    private GAME_STATE previousState = GAME_STATE.GAME_STATE_MAX;

    [SerializeField]
    private int gameMainScore;
	private int gameMainTaps;

    private GameController() {
        // Singleton
    }

    void Start() {
        if (instance) {
            Destroy(gameObject);
            Debug.LogError("Already initialised");
        } else {
            GameController.instance = this;
        }

        if (PlayerPrefs.GetInt("score", 0) < 0)
            PlayerPrefs.SetInt("score", 0);
		
		if (PlayerPrefs.GetInt("taps", 0) < 0)
			PlayerPrefs.SetInt("taps", 5);

        gameMainScore = PlayerPrefs.GetInt("score");
		gameMainTaps=PlayerPrefs.GetInt("taps");

        currentState = GAME_STATE.GAME_WELCOME;
        previousState = GAME_STATE.GAME_STATE_MAX;
    }

    public static GameController GetInstance() {
        return instance;
    }

    public void AddScore(int _score) {
        gameMainScore += _score;
    }

    public void ReduceScore(int _score) {
        gameMainScore -= _score;
    }

	public void AddTaps(int _taps){
		gameMainTaps=_taps;
	}

//	public void ReduceTaps(int _taps){
//		gameMainTaps-=_taps;
//	}

    public int GetScore() {
        return gameMainScore;
    }
	public int GetTouchCount() {
		return gameMainTaps;
	}

    public void ChangeGameState(GAME_STATE _state) {
        previousState = currentState;
        currentState = _state;
        UIController.GetInstance().OnStateChange();
    }

    public GAME_STATE GetGameState() {
        return currentState;
    }
	void OnDisable(){
		PlayerPrefs.SetInt("score", gameMainScore);
		PlayerPrefs.SetInt("taps", gameMainTaps);

	}
}
