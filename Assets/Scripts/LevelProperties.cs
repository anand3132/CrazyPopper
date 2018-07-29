using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This class contain current Level Properties
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

    private void Start() {
        popperCount = gameObject.GetComponentsInChildren<PopperProperties>().Length;
        touchCount = 0;
        currentLevelScore = (totalLevelScore * levelId) - (touchCount * 2);
		Debug.Log("Level id"+levelId);
		if(levelId==1)
			totalTouches=5;
		else
			totalTouches=GameController.GetInstance().GetTouchCount();
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
			AudioController.GetInstance().PlayAudio(AudioController.GetInstance().au_Level_Complete);
		}
		else{
			UIController.GetInstance().popUpMessage.text = " You Lost!! "+popperCount+" Still left" ;
			AudioController.GetInstance().PlayAudio(AudioController.GetInstance().au_Level_Failed);

		}
	}

    public void IncrementTouchCountByOne() {
        touchCount++;

        if (touchCount > totalTouches) {
            if (popperCount == 0) {
                currentLevelScore = (totalLevelScore * levelId) - (touchCount * 2);
				isLevelCleared=true;
			} else {
				isLevelCleared=false;
			}
				StartCoroutine("ShowResultsAfterDelay", ENDDELAY);

			Debug.Log("popperCount " + popperCount+"TouchCount "+touchCount);
		} else if(popperCount==0) {
			isLevelCleared=true;
			StartCoroutine("ShowResultsAfterDelay", ENDDELAY);
		}
		else{
			Debug.Log(" Total touch "+touchCount+" + "+totalTouches+" popperCount "+popperCount);

		}
    }

    public bool IsPoperAllowed() {
        return touchCount < totalTouches;
    }
}
