using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopperProperties : MonoBehaviour {
	
	public enum POPPER_TYPE{
		POPER_PURPLE
		,POPER_YELLOW
		,POPER_BLUE
	}
	//Poper default values
	[SerializeField]
	 POPPER_TYPE currentPoperType=POPPER_TYPE.POPER_PURPLE;
	[SerializeField]
	public int maxHit=1;
	public GameObject explotion;
	public GameObject ProjectileParent;
	// Use this for initialization
	void Start () {
		if(currentPoperType==POPPER_TYPE.POPER_PURPLE)
		{
			maxHit=1;
		}
		else if(currentPoperType==POPPER_TYPE.POPER_YELLOW)
		{
			maxHit=2;
		}
		else if(currentPoperType==POPPER_TYPE.POPER_BLUE)
		{
			maxHit=3;
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setPoperType(POPPER_TYPE setType){
		currentPoperType=setType;
	}

	public POPPER_TYPE getpoperType(){
		return currentPoperType;
	}

}
