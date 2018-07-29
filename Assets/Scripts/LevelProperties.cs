using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This class contain current Level Properties
public class LevelProperties : MonoBehaviour {
    public int currentLevelID;
    private int currentLevelScore;
    private int currentTouchCount;

    //Each level contain specific no of touches and score
    public int givenLevelScore;
    public int givenLevelTouch;

    private int totalLevelTouches;
    private int currentpopperCount;
    public bool isLevelCleared;
    private const float ENDDELAY = 3.0f;
    public static LevelProperties instance;

    private LevelProperties() {
        // Singleton
    }

    public static LevelProperties GetInstance() {
        return instance;
    }

    private void Start() {
        // score for the current loaded level.
        currentTouchCount = 0;
        currentpopperCount = gameObject.GetComponentsInChildren<PopperProperties>().Length;
        currentLevelScore = (givenLevelScore * currentLevelID) - (currentTouchCount * 2);

        Debug.Log("Level id" + currentLevelID);
        totalLevelTouches = (GameController.GetInstance().GetTouchCount() + givenLevelTouch);
        isLevelCleared = false;

        if (instance) {
            Destroy(gameObject);
            Debug.LogError("Already initialised");
        } else {
            LevelProperties.instance = this;
        }
    }

    public void DecrementPopperCountByOne() {
        --currentpopperCount;
        if (currentpopperCount < 0) {
            currentpopperCount = 0;
            Debug.LogError("popperCount < 0, reseting to 0.");
        }
        if (currentpopperCount == 0) {
            isLevelCleared = true;
            StartCoroutine("ShowResultsAfterDelay", ENDDELAY);
        }
    }

    public void IncrementTouchCountByOne() {
        currentTouchCount++;

        if (currentTouchCount >= totalLevelTouches) {
            if (currentpopperCount == 0) {
                currentLevelScore = (givenLevelScore * currentLevelID) - (currentTouchCount * 2);
                isLevelCleared = true;
            } else {
                isLevelCleared = false;
            }
            StartCoroutine("ShowResultsAfterDelay", ENDDELAY);

            Debug.Log("popperCount " + currentpopperCount + "TouchCount " + currentTouchCount);
        } else if (currentpopperCount == 0) {
            isLevelCleared = true;
            StartCoroutine("ShowResultsAfterDelay", ENDDELAY);
        } else {
            Debug.Log(" Total touch " + currentTouchCount + " + " + totalLevelTouches + " popperCount " + currentpopperCount);
        }
    }

    public int GetLevelScore() {
        return currentLevelScore;
    }

    public int GetLevelTouch() {
		int tmp=totalLevelTouches - currentTouchCount;
		return (tmp < 0) ? 0 : tmp;
    }

    // delay the result screen.
    IEnumerator ShowResultsAfterDelay(float delay) {
        yield return new WaitForSeconds(delay);
        GameController.GetInstance().ChangeGameState(GameController.GAME_STATE.GAME_RESULTS);
        if (isLevelCleared) {
            UIController.GetInstance().popUpMessage.text = " Level Cleared !! ";
            AudioController.GetInstance().PlayAudio(AudioController.GetInstance().au_Level_Complete);
        } else {
            UIController.GetInstance().popUpMessage.text = " You Lost!! " + currentpopperCount + " Still left";
            AudioController.GetInstance().PlayAudio(AudioController.GetInstance().au_Level_Failed);
        }
    }

    public bool IsPoperAllowed() {
        return currentTouchCount < totalLevelTouches;
    }
}
