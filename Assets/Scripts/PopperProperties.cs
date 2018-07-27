﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopperProperties : MonoBehaviour {
	
	public enum POPPER_TYPE{
		POPPER_PURPLE
		,POPPER_YELLOW
		,POPPER_BLUE
		,TYPE_MAX
	}
	public enum POPPER_STATE{
		ACTIVE
		,INACTIVE
		,DESTROY
		,STATE_MAX
	}
	//Poper default values
	 public POPPER_TYPE currentPoperType=POPPER_TYPE.POPPER_PURPLE;
	[SerializeField]
	int life=1;
	int currentHit;

	public GameObject bluePopper;
	public GameObject yelloPopper;
	public GameObject purplePopper;

	public GameObject explotion;
	public GameObject projectileParent;
	public GameObject currentPopper;

	public GameObject leftEye;
	public GameObject rightEye;
	public float eyeScale = 1.0f;
	public float eyeScaleAnimSpeed = 1.0f;
	public float eyeScaleAnimThreshold = 0.5f;
	Vector3 leftEyeInitScale;
	Vector3 rightEyeInitScale;

	public float plukScale = 0.2f;
	public float plukScaleSpeed = 1.5f;
	public Vector3 initScale;

	public POPPER_STATE currentState = POPPER_STATE.STATE_MAX;
	public POPPER_STATE prevState = POPPER_STATE.STATE_MAX;

	public void SetState(POPPER_STATE _state) {
		if (currentState!=_state) {
			POPPER_STATE _prevState = currentState;
			currentState = _state;
			prevState = _prevState;
			OnStateChange(_prevState);
		}
	}

	protected void OnStateChange(POPPER_STATE _from) {
		switch (currentState) {
		case POPPER_STATE.ACTIVE: {
				// changing state to active.
				break;
			}
		case POPPER_STATE.INACTIVE: {
				// changing state to inactive.
				break;
			}
		case POPPER_STATE.DESTROY: {
				// changing state to destroy.
				currentPopper.SetActive(false);
				LevelProperties.GetInstance().DecrementPopperCountByOne();
				gameObject.GetComponentInChildren<ProjectileController>().startExplotion();
				explotion.SetActive(true);
				SetState(POPPER_STATE.INACTIVE);
				break;
			}
		}
	}

	// Use this for initialization
	void Start () {
		bluePopper.gameObject.SetActive(false);
		yelloPopper.gameObject.SetActive(false);
		purplePopper.gameObject.SetActive(false);
		if(currentPoperType==POPPER_TYPE.POPPER_PURPLE)
		{
			life=1;
			currentPopper=purplePopper;
		}
		else if(currentPoperType==POPPER_TYPE.POPPER_YELLOW)
		{
			life=2;
			currentPopper=bluePopper;
		}
		else if(currentPoperType==POPPER_TYPE.POPPER_BLUE)
		{
			life=3;
			currentPopper=yelloPopper;
		}
		currentHit=life;
		currentPopper.SetActive(true);
		leftEyeInitScale = leftEye.transform.localScale;
		rightEyeInitScale = rightEye.transform.localScale;

		initScale = gameObject.transform.localScale;

		eyeScale = Random.Range(0.2f, eyeScaleAnimThreshold);
		eyeScaleAnimSpeed = Random.Range(0.1f, eyeScaleAnimSpeed);
		SetState(POPPER_STATE.ACTIVE);
	}
	
	// Update is called once per frame
	void Update () {
		OnInput();

		switch (currentState) {
		case POPPER_STATE.ACTIVE: {
				eyeScale+=eyeScaleAnimSpeed*Time.deltaTime;
				if (eyeScale>eyeScaleAnimThreshold) {
					eyeScale = -eyeScale;
				}
				var absScale = Mathf.Abs(eyeScale);
				leftEye.transform.localScale = leftEyeInitScale + Vector3.one*absScale;
				rightEye.transform.localScale = rightEyeInitScale + Vector3.one*-absScale;
				break;
			}
		}

		if (IsActive() || currentState== POPPER_STATE.INACTIVE) {
			// pluck
			if (plukScale>0.0f) {
				plukScale-=plukScaleSpeed*Time.deltaTime;
				if (plukScale<0.0f) {
					plukScale=0.0f;
				}
				Debug.Log("pluk "+plukScale);
				gameObject.transform.localScale = initScale + Vector3.one*plukScale;
			}
		}
	}

	public void DoPluk() {
		plukScale = 0.45f;
	}

	public void setPoperType(POPPER_TYPE setType){
		currentPoperType=setType;
	}

	public POPPER_TYPE getpoperType(){
		return currentPoperType;
	}
	void OnInput(){
		if (!IsActive()) {
			return;
		}

		if(Input .GetMouseButtonUp(0)||Input.touchCount>0){
			Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
			if (hit != null && hit.collider != null) {
				var hitObject = hit.collider.gameObject.GetComponentInChildren<ProjectileController>();
				if(hitObject!=null && hit.collider.gameObject==gameObject)
				{
					Debug.Log ("I'm hitting "+hit.collider.name + " , " + gameObject.name);
					ReduceLifeByOne();
					LevelProperties.GetInstance().IncrementTouchCountByOne();
					//GameController.GetInstance().AddScore()
				}
			}
			else{
				Debug.Log ("No Hit");
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.transform.parent.parent.gameObject == gameObject || !IsActive()) {
			return;
		}
		Debug.Log("Got hit from "+col.gameObject.name + " on "+gameObject.name);
		col.gameObject.SetActive(false);
		ReduceLifeByOne();
	}

	public void ReduceLifeByOne() {
		if (life<0 || !IsActive()) {
			Debug.LogError("Life is <=0 or state is not active. "+gameObject.name +" state "+currentState);
			return;
		}

		Debug.Log("Reduce life by one for "+gameObject.name +" state "+currentState);
		life--;
		OnReduceLife();
		if (life<=0) {
			SetState(POPPER_STATE.DESTROY);
		}
	}

	private void OnReduceLife() {
		DoPluk();
	}

	public bool IsActive() {
		return currentState == POPPER_STATE.ACTIVE;
	}
}
