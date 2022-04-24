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
        yield return new WaitForSeconds(9);
        Instantiate(explosion, transform.position, Quaternion.identity); 
        Object.Destroy(this.gameObject);
    }
}
