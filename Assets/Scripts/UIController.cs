using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

	public  UIController instance=null;

	private UIController(){}

	public UIController GetInstance(){
		if(instance==null){
			instance=new UIController();
		}
		return instance;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
