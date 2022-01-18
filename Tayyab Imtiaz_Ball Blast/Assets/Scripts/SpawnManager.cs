using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] ballPrefabs;
    public float startDelay;
    public float ballSpawnRate;
    private int ballIndex;
    private Vector3 spawnPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("InstantiateBalls", startDelay, ballSpawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This method is used to Instantiate random balls above screen
    void InstantiateBalls()
    {
        ballIndex = Random.Range(0, ballPrefabs.Length);
        spawnPoint = new Vector3(Random.Range(-15f, 15f), 15, 0);
        Instantiate(ballPrefabs[ballIndex], spawnPoint, ballPrefabs[ballIndex].transform.rotation);
    }
}
