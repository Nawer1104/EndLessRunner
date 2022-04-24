using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningTile : MonoBehaviour
{
    public GameObject tileToSpawn;
    public GameObject spawningTile;
    public float timeOffset = 0.3f;
    public float distanceBetweenTiles = 4.5F;
    public float randomValue = 0.8f;
    private Vector3 previousTilePosition;
    private float startTime;
    private Vector3 direction, mainDirection = new Vector3(0, 0, 1), otherDirection = new Vector3(1, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
 
        previousTilePosition = spawningTile.transform.position;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime > timeOffset)
        {
            //if (Random.value < randomValue)
            //    direction = mainDirection;
            //else
            //{
            //    Vector3 temp = direction;d
            //    direction = otherDirection;
            //    mainDirection = direction;
            //    otherDirection = temp;
            //}
            Vector3 spawnPos = previousTilePosition + distanceBetweenTiles * mainDirection;
            startTime = Time.time;
            Instantiate(tileToSpawn, spawnPos, Quaternion.Euler(0, 0, 0));
            previousTilePosition = spawnPos;
        }
    }

    
}
