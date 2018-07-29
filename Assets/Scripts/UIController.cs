using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This class controls diffrent UI Behaviors in the game
public class UIController : MonoBehaviour {
    public static UIController instance;
    public GameObject levelSelectMenu;
    public GameObject popUpMenu;
    public GameObject HUDMenue;
    public GameObject WelcomeMenu;
	[Space]
    public Text mainScoreText;
    public Text touchCount;
    public Text currentLevelScore;
	public Text popUpMessage;
	[Space]
	public Button HUDmuteButton;
	public Button pauseButton;
	private bool  pauseSwitch=false;
	private bool muteSwitch=false;
	private float welcomScreenDelay=2f;

    private UIController() {
        // Singleton
    }

    public static UIController GetInstance() {
        return instance;
    }

    private void Start() {
        if (instance) {
            Destroy(gameObject);
            Debug.LogError("UIController Already initialised");
        } else {
            UIController.instance = this;
        }
        ResetMenue();
		StartCoroutine("WelcomeScreen", welcomScreenDelay);
        mainScoreText.text = GameController.GetInstance().GetScore().ToString();
    }

    private void Update() {
        if (GameController.GetInstance().GetGameState() == GameController.GAME_STATE.GAME_GAMEPLAY &&
           LevelProperties.GetInstance() != null) {
            currentLevelScore.text = LevelProperties.GetInstance().currentLevelScore.ToString();
			int tmp=(LevelProperties.GetInstance().totalTouches - LevelProperties.GetInstance().touchCount);
			touchCount.text = (tmp < 0 ? 0 : tmp).ToString ();
        } 
    }

	//show Welcome screen with Delay time
	IEnumerator WelcomeScreen(float delay) {
		WelcomeMenu.SetActive(true);
		yield return new WaitForSeconds(delay);
		GameController.GetInstance().ChangeGameState(GameController.GAME_STATE.GAME_LEVEL_SELECT);
	}

	//level Selection button click events
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
		
	public void MuteOnClick(){
		Debug.Log("onclicked");
		if(muteSwitch){
			AudioController.GetInstance().PlayBGM();
			muteSwitch=!muteSwitch;
			HUDmuteButton.GetComponent<Image>().color=Color.green;
		}
		else
		{
			AudioController.GetInstance().PauseBGM();
			muteSwitch=!muteSwitch;
			HUDmuteButton.GetComponent<Image>().color=Color.red;

		}
	}

	public void PauseOnClick(){
		if(pauseSwitch) {	
			pauseSwitch= !pauseSwitch;
			pauseButton.GetComponentInChildren<Text>().fontStyle=FontStyle.Normal;
			pauseButton.GetComponentInChildren<Text>().color=Color.red;
			pauseButton.GetComponent<Image>().color=Color.green;
			GameController.GetInstance().ChangeGameState(GameController.GAME_STATE.GAME_GAMEPLAY);

		} else{
			pauseSwitch= !pauseSwitch;
			pauseButton.GetComponentInChildren<Text>().fontStyle=FontStyle.Bold;
			pauseButton.GetComponentInChildren<Text>().color=Color.green;
			pauseButton.GetComponent<Image>().color=Color.red;
			GameController.GetInstance().ChangeGameState(GameController.GAME_STATE.Game_PAUSE);
		}
	}

    private void ResetMenue() {
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
			case GameController.GAME_STATE.Game_PAUSE:
			{
				HUDMenue.SetActive(true);
				mainScoreText.text = GameController.GetInstance().GetScore().ToString();
				touchCount.text=GameController.GetInstance().GetTouchCount().ToString();
			}
                break;
        }
    }
}
