using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopperProperties : MonoBehaviour {
	
    public enum POPPER_TYPE {
        POPPER_PURPLE
        , POPPER_YELLOW
        , POPPER_BLUE
        , TYPE_MAX
    }
    public enum POPPER_STATE {
        ACTIVE
        , INACTIVE
        , DESTROY
        , STATE_MAX
    }

    //Poper default values
    public POPPER_TYPE currentPoperType = POPPER_TYPE.POPPER_PURPLE;
    [SerializeField]
    int life = 1;

    private bool levelDelayer;
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

	private const float ENABLEPOPPERAFTER = 1.5f;

    public POPPER_STATE currentState = POPPER_STATE.STATE_MAX;
    public POPPER_STATE prevState = POPPER_STATE.STATE_MAX;

    void Awake() {
        levelDelayer = true;
    }

    void Start() {
        ResetPopper();
        levelDelayer = true;
        if (currentPoperType == POPPER_TYPE.POPPER_PURPLE) {
            life = 1;
            currentPopper = purplePopper;
        } else if (currentPoperType == POPPER_TYPE.POPPER_YELLOW) {
            life = 2;
            currentPopper = bluePopper;
        } else if (currentPoperType == POPPER_TYPE.POPPER_BLUE) {
            life = 3;
            currentPopper = yelloPopper;
        }
        currentPopper.SetActive(true);
        leftEye.SetActive(true);
        rightEye.SetActive(true);

        leftEyeInitScale = leftEye.transform.localScale;
        rightEyeInitScale = rightEye.transform.localScale;

        initScale = gameObject.transform.localScale;

        eyeScale = Random.Range(0.2f, eyeScaleAnimThreshold);
        eyeScaleAnimSpeed = Random.Range(0.1f, eyeScaleAnimSpeed);
        SetState(POPPER_STATE.ACTIVE);

		StartCoroutine("EnablePopperAfterDelay", ENABLEPOPPERAFTER);
    }

    public void SetState(POPPER_STATE _state) {
        if (currentState != _state) {
            POPPER_STATE _prevState = currentState;
            currentState = _state;
            prevState = _prevState;
            OnStateChange(_prevState);
        }
    }

    protected void OnStateChange(POPPER_STATE _from) {
        switch (currentState) {
            case POPPER_STATE.DESTROY: {
                // changing state to destroy.
                currentPopper.SetActive(false);
                projectileParent.SetActive(true);
                gameObject.GetComponentInChildren<ProjectileController>().StartExplotion();
                SetState(POPPER_STATE.INACTIVE);
				break;
                }
        }
    }

    void OnPopperHit() {
        ReduceLifeByOne();
        currentPopper.SetActive(false);
        switch (life) {
            case 0: {
					LevelProperties.GetInstance().DecrementPopperCountByOne();
                    currentPopper = explotion;
                    SetState(POPPER_STATE.DESTROY);
					AudioController.GetInstance().PlayAudio(AudioController.GetInstance().au_Popup);
                }
                break;
            case 1: {
                    currentPopper = purplePopper;
                }
                break;
            case 2: {
                    currentPopper = bluePopper;
                }
                break;
            default : {
                    Debug.Log("Not implemented");
                    break;
            }
        }
        currentPopper.SetActive(true);
    }

    void ResetPopper() {
        bluePopper.gameObject.SetActive(false);
        yelloPopper.gameObject.SetActive(false);
        purplePopper.gameObject.SetActive(false);
        explotion.gameObject.SetActive(false);
        leftEye.gameObject.SetActive(false);
        rightEye.gameObject.SetActive(false);
        projectileParent.gameObject.SetActive(false);
    }

    void Update() {
		if(GameController.GetInstance().GetGameState()!=GameController.GAME_STATE.Game_PAUSE){
	        OnInput();
	        switch (currentState) {
	            case POPPER_STATE.ACTIVE: {
	                    eyeScale += eyeScaleAnimSpeed * Time.deltaTime;
	                    if (eyeScale > eyeScaleAnimThreshold) {
	                        eyeScale = -eyeScale;
	                    }
	                    var absScale = Mathf.Abs(eyeScale);
	                    leftEye.transform.localScale = leftEyeInitScale + Vector3.one * absScale;
	                    rightEye.transform.localScale = rightEyeInitScale + Vector3.one * -absScale;
	                    break;
	                }
	        }

	        if (IsActive() || currentState == POPPER_STATE.INACTIVE) {
	            // pluck
	            if (plukScale > 0.0f) {
	                plukScale -= plukScaleSpeed * Time.deltaTime;
	                if (plukScale < 0.0f) {
	                    plukScale = 0.0f;
	                }
	                gameObject.transform.localScale = initScale + Vector3.one * plukScale;
	            }
	        }
		}
    }

    public void DoPluk() {
        plukScale = 0.45f;
    }

    public void SetPoperType(POPPER_TYPE setType) {
        currentPoperType = setType;
    }

    public POPPER_TYPE getpoperType() {
        return currentPoperType;
    }

    IEnumerator EnablePopperAfterDelay(float delay) {
        yield return new WaitForSeconds(delay);
        levelDelayer = false;
    }

    void OnInput() {
        if (!IsActive() || levelDelayer) {
            return;
        }

        if (Input.GetMouseButtonUp(0) || Input.touchCount > 0
            && GameController.GetInstance().GetGameState() == GameController.GAME_STATE.GAME_GAMEPLAY) {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit.collider != null) {
                var hitObject = hit.collider.gameObject.GetComponentInChildren<ProjectileController>(true);
                if (hitObject != null && hit.collider.gameObject == gameObject) {
                    OnPopperHit();
					LevelProperties.GetInstance().IncrementTouchCountByOne();
                }
            } else {
                Debug.Log("No Hit ");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.transform.parent.parent.gameObject == gameObject || !IsActive() || levelDelayer) {
            return;
        }
        col.gameObject.SetActive(false);
		//col.gameObject.GetComponent<PopperProperties>().SetState(po);
        OnPopperHit();
    }

    private void ReduceLifeByOne() {
        if (life < 0 || !IsActive()) {
            Debug.LogError("Life is <=0 or state is not active. " + gameObject.name + " state " + currentState);
            return;
        }

        life--;
        OnReduceLife();
        if (life <= 0) {
            SetState(POPPER_STATE.DESTROY);
        }
    }

    private void OnReduceLife() {
        DoPluk();
    }

    public bool IsActive() {
        return (currentState == POPPER_STATE.ACTIVE &&
            GameController.GetInstance().GetGameState() == GameController.GAME_STATE.GAME_GAMEPLAY);
    }
}
