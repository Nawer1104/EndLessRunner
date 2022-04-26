using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField]
    private GameObject footSlam;

    private bool coroutineAllowed, grounded;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            coroutineAllowed = true;
            Instantiate(footSlam, transform.position, footSlam.transform.rotation);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = false;
            coroutineAllowed = false;
        }
    }

    void Update()
    {
        if (grounded && coroutineAllowed)
        {
            StartCoroutine(SpawnFootSlam());
            coroutineAllowed = false;
        } 

        if (!grounded)
        {
            StopCoroutine(SpawnFootSlam());
            coroutineAllowed = true;
        }
    }

    IEnumerator SpawnFootSlam()
    {
        while(grounded)
        {
            Instantiate(footSlam, transform.position, footSlam.transform.rotation);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
