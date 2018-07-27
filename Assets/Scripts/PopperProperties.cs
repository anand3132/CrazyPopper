using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopperProperties : MonoBehaviour {
	
	public enum POPPER_TYPE{
		POPER_PURPLE
		,POPER_YELLOW
		,POPER_BLUE
	}
	public enum POPPER_STATE{
		INACTIVE
		,ACTIVE
		,DESTROY
	}
	//Poper default values
	 public POPPER_TYPE currentPoperType=POPPER_TYPE.POPER_PURPLE;
	[SerializeField]
	int maxHit=1;
	int currentHit;
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
		currentHit=maxHit;
	}
	
	// Update is called once per frame
	void Update () {
		OnInput();
		if(currentHit==0){
			
		}
	}

	public void setPoperType(POPPER_TYPE setType){
		currentPoperType=setType;
	}

	public POPPER_TYPE getpoperType(){
		return currentPoperType;
	}
	void OnInput(){
		if(Input .GetMouseButton(0)||Input.touchCount>0){
			Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

			if (hit != null && hit.collider != null) {
				Debug.Log ("I'm hitting "+hit.collider.name);
				hit.collider.gameObject.GetComponent<ProjectileController>().startExplotion();
			}
			else{
				Debug.Log ("No Hit");

			}
		}
	}

}
