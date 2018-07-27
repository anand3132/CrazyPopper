using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProperties : MonoBehaviour {

	public int currentLevelScore;
	public int totalLevelScore=30;
	public int totalTouches;
	public int levelId;
	public int touchCount;
	public int popperCount;

	void Start(){
		popperCount=gameObject.GetComponentsInChildren<PopperProperties>().Length;
		touchCount=0;
		levelId=1;
		totalTouches=2;
		currentLevelScore=0;


		if(instance) {
			GameObject.Destroy(gameObject);
			Debug.LogError("Already initialised");
		} else {
			LevelProperties.instance = this;
		}
	}

	public void DecrementPopperCountByOne() {
		popperCount--;
		if (popperCount < 0) {
			popperCount = 0;
			Debug.LogError("popperCount < 0, reseting to 0.");
		}
	}

	public void IncrementTouchCountByOne() {
		touchCount++;
		if (touchCount > totalTouches) {
			GameController.GetInstance().changeGameState(GameController.GAME_STATE.GAME_RESULTS);
			currentLevelScore=(totalLevelScore*levelId)-(touchCount*2);
			Debug.Log("Current Level Score: "+currentLevelScore);
		}
	}
	public bool IsPoperAllowed () {
		return touchCount < totalTouches;
	}

	private LevelProperties(){}
	public static LevelProperties instance;
	public static LevelProperties GetInstance(){
		return instance;
	}
}
