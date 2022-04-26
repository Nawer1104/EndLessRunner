using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTiles : MonoBehaviour
{
    public GameObject explosion;    

    private void Awake()
    {
        StartCoroutine(destroy());
    }

    IEnumerator destroy()
    {
        yield return new WaitForSeconds(15);
        explosion = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject; 
        Object.Destroy(this.gameObject);
        Destroy(explosion, 5f);
    }
}
