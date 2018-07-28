using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour {
    public GameObject currentLoadedLevel = null;
    public static LevelSelection instance;
    public const int MAXLEVEL = 8;

    void Start() {
        if (instance) {
            Destroy(gameObject);
            Debug.LogError("Already initialised");
        } else {
            LevelSelection.instance = this;
        }
        if (GameController.GetInstance())
            UnlockLevel(GameController.GetInstance().GetScore());
    }

    public static LevelSelection GetInstance() {
        return instance;
    }

    public int GetMaxLevel() {
        return MAXLEVEL;
    }

    public void LoadLevel(int level) {
        if (level>MAXLEVEL) {
            Debug.LogError("level>MAXLEVEL");
            return;
        }

        if (currentLoadedLevel) {
            currentLoadedLevel.SetActive(false);
        }
        currentLoadedLevel = Instantiate(Resources.Load(("Levels/Level" + level).Trim()), gameObject.transform) as GameObject;
		currentLoadedLevel.GetComponent<LevelProperties>().levelId=level;
        if (currentLoadedLevel==null) {
            Debug.LogError("Level load error.");
            return;
        }
        currentLoadedLevel.SetActive(true);
    }

    public void UnloadCurrentLevel() {
        if (currentLoadedLevel) {
            currentLoadedLevel.SetActive((false));
            Destroy(currentLoadedLevel);
            currentLoadedLevel = null;
        }
    }

    public void UnlockLevel(int _score) {
        Button[] levelSelector = UIController.GetInstance().levelSelectMenu.gameObject.GetComponentsInChildren<Button>();

        if (_score > -1) {
            levelSelector[0].interactable = true;
        }
        if (_score > 20) {
            levelSelector[1].interactable = true;
        }
        if (_score > 50) {
            levelSelector[2].interactable = true;
        }
        if (_score > 150) {
            levelSelector[3].interactable = true;
        }
        if (_score > 200) {
            levelSelector[4].interactable = true;
        }
        if (_score > 340) {
            levelSelector[5].interactable = true;
        }
        if (_score > 400) {
            levelSelector[6].interactable = true;
        }
    }

    public void OnLevelExit(bool isScoreUpdated) {
		int touchesleft=LevelProperties.GetInstance().totalTouches-LevelProperties.GetInstance().touchCount;
		if (isScoreUpdated){
            GameController.GetInstance().AddScore(LevelProperties.GetInstance().currentLevelScore);
				GameController.GetInstance().AddTaps(touchesleft);
				Debug.Log("Adde touches "+touchesleft);
		}
        UnloadCurrentLevel();
        GameController.GetInstance().ChangeGameState(GameController.GAME_STATE.GAME_LEVEL_SELECT);
    }
}
