using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileProperties : MonoBehaviour {

	public enum PRO_DRIECTION{
		LEFT
		,RIGHT
		,TOP
		,BOTTOM
	}

	public  Vector3 direction;
	public float speed=3;
	//set default direction to top
	public PRO_DRIECTION currentDirection=PRO_DRIECTION.TOP;

	void Start () {
		if(currentDirection==PRO_DRIECTION.LEFT)
			direction=Vector3.left;
		else if(currentDirection==PRO_DRIECTION.RIGHT)
			direction=Vector3.right;
		else if(currentDirection==PRO_DRIECTION.TOP)
			direction=Vector3.up;
		else if(currentDirection==PRO_DRIECTION.BOTTOM)
			direction=Vector3.down;
	}

	void setDirection(PRO_DRIECTION dir)
	{
		currentDirection=dir;
	}

	void Update () {
		gameObject.transform.Translate(direction*Time.deltaTime*speed);
	}
}
