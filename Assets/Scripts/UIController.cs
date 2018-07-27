using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour {

	public  UIController instance=null;
	public GameController gameController=null;
	private UIController(){}

	public UIController GetInstance(){
		if(instance==null){
			instance=new UIController();
		}
		return instance;
	}

	public Text scoreText;
	public Text touchCount;


	// Use this for initialization
	void Start () {
		//scoreText.text=GameController.GetInstance().GetScore().ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LevelOnClick(int selectedLevel)
	{
		if(selectedLevel >GameController.GetInstance().MAXLEVEL){
			Debug.Log("Max Level");
		}
		switch(selectedLevel){
		case 1:
			{
				//Instantiate();
			}
			break;
		}
	}

}
