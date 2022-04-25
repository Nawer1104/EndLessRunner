using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

namespace EpicToonFX
{
public class ETFXFireProjectile : MonoBehaviour 
{
    public GameObject[] projectiles;
    public Transform spawnPosition;
    [HideInInspector]
    public int currentProjectile = 0;
	public float speed = 1000;




	void Update () 
	{
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

                GameObject projectile = Instantiate(projectiles[currentProjectile], spawnPosition.position, Quaternion.identity) as GameObject;
               
                projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * speed);

        }
	}

}
}