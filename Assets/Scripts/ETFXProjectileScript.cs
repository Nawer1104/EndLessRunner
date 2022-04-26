using UnityEngine;
using System.Collections;
 
public class ETFXProjectileScript : MonoBehaviour
{
    public GameObject impactParticle;
    public GameObject projectileParticle;
    public GameObject muzzleParticle;
    public float lifeTimePracticle;

    [HideInInspector]
    public Vector3 impactNormal; //Used to rotate impactparticle.
 
    private bool hasCollided = false;
 
    void Start()
    {
        Invoke(nameof(DestroyProjectile), lifeTimePracticle);
        projectileParticle = Instantiate(projectileParticle, transform.position, transform.rotation) as GameObject;
        projectileParticle.transform.parent = transform;
		if (muzzleParticle){
        muzzleParticle = Instantiate(muzzleParticle, transform.position, transform.rotation) as GameObject;
        Destroy(muzzleParticle, 1.5f); // Lifetime of muzzle effect.
		}
    }
 
    void OnCollisionEnter(Collision hit)
    {
        if (!hasCollided)
        {
            hasCollided = true;
            impactParticle = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;
 
            if (hit.gameObject.CompareTag("Enemy")) // Projectile will destroy objects tagged as Enemy
            {
                Destroy(hit.gameObject);
            }
 
 
            
            Destroy(projectileParticle, 3f);
            Destroy(impactParticle, 5f);
            DestroyProjectile();
            //projectileParticle.Stop();


        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}