using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour {

	public List<GameObject> levels = new List<GameObject>();
	public GameObject currentLoadedLevel = null;
	public static LevelSelection instance;

	public void LoadLevel(int level) {
		if (level>=levels.Count) {
			Debug.LogError("Level index not valid "+ level);
			return;
		}

		if (currentLoadedLevel) {
			currentLoadedLevel.SetActive(false);
		}
		Debug.Log(("Levels/Level"+level).Trim());
		currentLoadedLevel = Instantiate(Resources.Load(("Levels/Level"+level).Trim() ),gameObject.transform) as GameObject; 
		currentLoadedLevel.SetActive(true);
	}

	public void UnloadCurrentLevel() {
		if (currentLoadedLevel) {
			currentLoadedLevel.SetActive((false));
			GameObject.Destroy(currentLoadedLevel);
			currentLoadedLevel = null;
		}
	}

	public void UnlockLevel(int _score){
		Button[] levelSelector =UIController.GetInstance().levelSelectMenu.gameObject.GetComponentsInChildren<Button>();

		if(_score>-1)
		{
			levelSelector[0].interactable=true;
		}
		if(_score>20)
		{
			levelSelector[1].interactable=true;
		}
		if(_score>40)
		{
			levelSelector[2].interactable=true;
		}
		if(_score>80)
		{
			levelSelector[3].interactable=true;
		}
		if(_score>100)
		{
			levelSelector[4].interactable=true;
		}
		if(_score>140)
		{
			levelSelector[5].interactable=true;
		}
		Debug.Log(_score);
	}

	public static LevelSelection GetInstance(){
		return instance;
	}

	public void OnLevelExit(bool isScoreUpdated){
		if(isScoreUpdated)
			GameController.GetInstance().AddScore(LevelProperties.GetInstance().currentLevelScore);
		UnloadCurrentLevel();
		GameController.GetInstance().ChangeGameState(GameController.GAME_STATE.GAME_LEVEL_SELECT);
	}

	void Start(){

		if(instance) {
			GameObject.Destroy(gameObject);
			Debug.LogError("Already initialised");
		} else {
			LevelSelection.instance = this;
		}
		if(GameController.GetInstance())
			UnlockLevel(GameController.GetInstance().GetScore());
	}

}
