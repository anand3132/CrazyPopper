using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class sets Projectile behavior Properties
public class ProjectileController : MonoBehaviour {
    public void StartExplotion() {
        ProjectileProperties.PRO_DRIECTION[] directions = {
            ProjectileProperties.PRO_DRIECTION.LEFT,  
            ProjectileProperties.PRO_DRIECTION.RIGHT, 
            ProjectileProperties.PRO_DRIECTION.TOP, 
            ProjectileProperties.PRO_DRIECTION.BOTTOM
        };

        for (int i = 0; i < directions.Length; i++) {
            if (gameObject.transform.childCount<=i) {
                Debug.LogError("Child count is less than directions. Please check the prefab.");
                break;
            }
            var projectile = gameObject.transform.GetChild(i).GetComponent<ProjectileProperties>();
            if (projectile) {
                projectile.gameObject.SetActive(true);
                projectile.currentDirection = directions[i];
            }
         }
    }

	public void OnTriggerEnter2D(Collider2D coll) {
        Debug.Log("someting hit");
    }
}
