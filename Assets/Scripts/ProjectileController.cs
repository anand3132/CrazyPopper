using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {
	
	void Start () {
		
	}

	public void startExplotion(){
		
		for(int i=0;i<4;i++)
		{
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
}
