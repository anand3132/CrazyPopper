using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class specifies Projectile behaviour
public class ProjectileProperties : MonoBehaviour {
	
    public enum PRO_DRIECTION {
        LEFT
        , RIGHT
        , TOP
        , BOTTOM
    }

    public Vector3 direction;
    public float speed = 3;
	private float distructionDelay=5f;
	private Transform startTranform;
    //set default direction to top
    public PRO_DRIECTION currentDirection = PRO_DRIECTION.TOP;

    void Start() {
		startTranform=gameObject.transform;
        if (currentDirection == PRO_DRIECTION.LEFT)
            direction = Vector3.left;
        else if (currentDirection == PRO_DRIECTION.RIGHT)
            direction = Vector3.right;
        else if (currentDirection == PRO_DRIECTION.TOP)
            direction = Vector3.up;
        else if (currentDirection == PRO_DRIECTION.BOTTOM)
            direction = Vector3.down;
		StartCoroutine("SelfDisable",distructionDelay);
    }

    void setDirection(PRO_DRIECTION dir) {
        currentDirection = dir;
    }

	//Dissable  the Projectile after every distructionDelay time
	IEnumerator SelfDisable(float _delay){
		bool tmp=GameController.GetInstance().GetGameState()==GameController.GAME_STATE.Game_PAUSE?true:false;
		//Checkes pause state
		if(tmp)
		{	yield return new WaitUntil (()=>tmp==false);
			Debug.Log("paused!!");
		}else{
			yield return new WaitForSeconds(_delay);
			Debug.Log("regular!!");

		}
		gameObject.SetActive(false);
		gameObject.transform.position= startTranform.position;
	}

    void Update() {
		if(GameController.GetInstance().GetGameState()!=GameController.GAME_STATE.Game_PAUSE){
        	gameObject.transform.Translate(direction * Time.deltaTime * speed);
		}
    }

	//checkes wether pojectile hit with poppers
	public void OnTriggerEnter2D(Collider2D coll) {
		if(coll.GetComponent<PopperProperties>() && coll.gameObject!=gameObject) {	
			Debug.Log("someting hit "+coll.name);
			//gameObject.SetActive(false);
		} else {
			Debug.Log("someting hit "+coll.name);
		}
	}

}
