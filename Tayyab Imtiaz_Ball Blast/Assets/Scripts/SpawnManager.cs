using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] ballPrefabs;
    public float startDelay;
    public float ballSpawnRate;
    private int ballIndex;
    private Vector3 spawnPoint;
    private PlayerController playerScript;
    
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        InvokeRepeating("InstantiateBalls", startDelay, ballSpawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This method is used to Instantiate random balls above screen
    void InstantiateBalls()
    {
        if(playerScript.gameOver == false)
        {
            ballIndex = Random.Range(0, ballPrefabs.Length);
            spawnPoint = new Vector3(Random.Range(-9f, 9f), 12, 0);
            GameObject ball = Instantiate(ballPrefabs[ballIndex], spawnPoint, ballPrefabs[ballIndex].transform.rotation);
            ball.GetComponent<BallScript>().InitializeBall(this);
        }  
    }

    public void ReloadMainScene()
    {
        SceneManager.LoadScene(0);
    }

    public void Instantiate2BallsWithImpulse(int id, Transform spawnTransform)
    {
        GameObject ball;
        ball = Instantiate(ballPrefabs[id - 1], spawnTransform.position, ballPrefabs[id - 1].transform.rotation);
        ball.GetComponent<BallScript>().InitializeBall(this);
        ball.GetComponent<Rigidbody>().AddForce(Vector3.left * 3, ForceMode.Impulse);
        ball = Instantiate(ballPrefabs[id - 1], spawnTransform.position, ballPrefabs[id - 1].transform.rotation);
        ball.GetComponent<BallScript>().InitializeBall(this);
        ball.GetComponent<Rigidbody>().AddForce(Vector3.right * 3, ForceMode.Impulse);
    }
}
