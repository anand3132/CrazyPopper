using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public static UIController instance;
    public GameObject levelSelectMenu;
    public GameObject popUpMenu;
    public GameObject HUDMenue;
    public GameObject WelcomeMenu;
    public Text mainScoreText;
    public Text touchCount;
    public Text currentLevelScore;
	public Text popUpMessage;
    private UIController() {
        // Singleton
    }

    public static UIController GetInstance() {
        return instance;
    }

    void Start() {
        if (instance) {
            Destroy(gameObject);
            Debug.LogError("UIController Already initialised");
        } else {
            UIController.instance = this;
        }
        ResetMenue();
        StartCoroutine("WelcomeScreen", 0.1f);
        mainScoreText.text = GameController.GetInstance().GetScore().ToString();
    }

    IEnumerator WelcomeScreen(float delay) {
        WelcomeMenu.SetActive(true);
        yield return new WaitForSeconds(delay);
        GameController.GetInstance().ChangeGameState(GameController.GAME_STATE.GAME_LEVEL_SELECT);
    }

    void Update() {
        if (GameController.GetInstance().GetGameState() == GameController.GAME_STATE.GAME_GAMEPLAY &&
           LevelProperties.GetInstance() != null) {
            currentLevelScore.text = LevelProperties.GetInstance().currentLevelScore.ToString();
            touchCount.text = ((LevelProperties.GetInstance().totalTouches -
				LevelProperties.GetInstance().touchCount)
				+GameController.GetInstance().GetTouchCount()).ToString();
        }
    }

    public void LevelOnClick(int selectedLevel) {
        if (selectedLevel > LevelSelection.GetInstance().GetMaxLevel()) {
            Debug.Log("Max Level" + selectedLevel + ">" + LevelSelection.GetInstance().GetMaxLevel());
        } else {
            LevelSelection.GetInstance().LoadLevel(selectedLevel);
            levelSelectMenu.SetActive(false);
            GameController.GetInstance().ChangeGameState(GameController.GAME_STATE.GAME_GAMEPLAY);
        }
    }

    public void PopupNextOnClick() {
		LevelSelection.GetInstance().OnLevelExit(LevelProperties.GetInstance().isLevelCleared);
    }

    void ResetMenue() {
        WelcomeMenu.SetActive(false);
        levelSelectMenu.SetActive(false);
        HUDMenue.SetActive(false);
        popUpMenu.SetActive(false);
    }

    public void OnStateChange() {
        var currentState = GameController.GetInstance().GetGameState();
        ResetMenue();
        switch (currentState) {
            case GameController.GAME_STATE.GAME_GAMEPLAY: {
                    HUDMenue.SetActive(true);
                    mainScoreText.text = GameController.GetInstance().GetScore().ToString();
					touchCount.text=GameController.GetInstance().GetTouchCount().ToString();
                }
                break;
            case GameController.GAME_STATE.GAME_LEVEL_SELECT: {
                    levelSelectMenu.SetActive(true);
                    LevelSelection.GetInstance().UnlockLevel(GameController.GetInstance().GetScore());
                }
                break;
            case GameController.GAME_STATE.GAME_RESULTS: {
                    popUpMenu.SetActive(true);
                }
                break;
        }
    }
}
