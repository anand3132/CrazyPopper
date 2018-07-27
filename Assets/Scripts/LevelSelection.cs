using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelection : MonoBehaviour {

	public List<GameObject> levels = new List<GameObject>();
	public GameObject currentLoadedLevel = null;

	public void LoadLevel(int level) {
		if (level>=levels.Count) {
			Debug.LogError("Level index not valid");
			return;
		}

		if (currentLoadedLevel) {
			currentLoadedLevel.SetActive(false);
		}
		levels[level].gameObject.SetActive(true);
		currentLoadedLevel = levels[level];
	}

	public void UnloadCurrentLevel() {
		if (currentLoadedLevel) {
			currentLoadedLevel.SetActive((false));
			currentLoadedLevel = null;
		}
	}

}
