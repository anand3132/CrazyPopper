using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {
	
	void Start () {
		
	}

	public void StartExplotion(){
		
		for(int i=0;i<4;i++)
		{
			gameObject.transform.GetChild(i).gameObject.SetActive(true);
			if(i==0)
				gameObject.transform.GetChild(i).GetComponent<ProjectileProperties>().currentDirection=ProjectileProperties.PRO_DRIECTION.LEFT;
			else if(i==1)
				gameObject.transform.GetChild(i).GetComponent<ProjectileProperties>().currentDirection=ProjectileProperties.PRO_DRIECTION.RIGHT;
			else if(i==2)
				gameObject.transform.GetChild(i).GetComponent<ProjectileProperties>().currentDirection=ProjectileProperties.PRO_DRIECTION.TOP;
			else if(i==3)
				gameObject.transform.GetChild(i).GetComponent<ProjectileProperties>().currentDirection=ProjectileProperties.PRO_DRIECTION.BOTTOM;
		}
	}

	public void OnCollisionEnter2D(Collision2D coll) 
		{
			Debug.Log("someting hit");
		}
}
