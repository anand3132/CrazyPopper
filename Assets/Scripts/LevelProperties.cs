using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelProperties : MonoBehaviour {
    public int currentLevelScore;
    public int totalLevelScore = 30;
    public int totalTouches;
    public int levelId;
    public int touchCount;
    public int popperCount;
	public bool isLevelCleared;
	private const float ENDDELAY = 1.0f;
    public static LevelProperties instance;

    private LevelProperties() {
        // Singleton
    }

    public static LevelProperties GetInstance() {
        return instance;
    }

    void Start() {
        popperCount = gameObject.GetComponentsInChildren<PopperProperties>().Length;
        touchCount = 0;
        levelId = 1;
        currentLevelScore = (totalLevelScore * levelId) - (touchCount * 2);
		isLevelCleared=false;

        if (instance) {
            Destroy(gameObject);
            Debug.LogError("Already initialised");
        } else {
            LevelProperties.instance = this;
        }
    }
    public void DecrementPopperCountByOne() {
        --popperCount;
        if (popperCount < 0) {
           // popperCount = 0;
            Debug.LogError("popperCount < 0, reseting to 0.");
        }
		if(popperCount==0){
			isLevelCleared=true;
			StartCoroutine("ShowResultsAfterDelay",ENDDELAY);
		}
    }

	IEnumerator ShowResultsAfterDelay(float delay) {
		yield return new WaitForSeconds(delay);
		GameController.GetInstance().ChangeGameState(GameController.GAME_STATE.GAME_RESULTS);
		if(isLevelCleared){
			UIController.GetInstance().popUpMessage.text = " Level Cleared !! ";
		}
		else{
			UIController.GetInstance().popUpMessage.text = " you Lost!!"+popperCount;
		}
	}

    public void IncrementTouchCountByOne() {
        touchCount++;

        if (touchCount >= totalTouches) {
            if (popperCount == 0) {
                currentLevelScore = (totalLevelScore * levelId) - (touchCount * 2);
				isLevelCleared=true;
				StartCoroutine("ShowResultsAfterDelay", ENDDELAY);
			}
			else
				isLevelCleared=false;

            Debug.Log("Current Level Score: " + currentLevelScore);
		} else if(popperCount==0) {
			StartCoroutine("ShowResultsAfterDelay", ENDDELAY);
			isLevelCleared=false;
		}
		else{
			Debug.Log(" Total touch "+touchCount+" popperCount "+popperCount);

		}
    }

    public bool IsPoperAllowed() {
        return touchCount < totalTouches;
    }
}
