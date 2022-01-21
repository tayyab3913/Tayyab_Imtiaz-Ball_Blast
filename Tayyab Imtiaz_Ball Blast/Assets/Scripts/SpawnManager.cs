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

    // This method reloads the main scene
    public void ReloadMainScene()
    {
        SceneManager.LoadScene(0);
    }

    // This method is called by the ball script everytime a ball collides with either player or fire
    public void Instantiate2BallsWithImpulse(int id, Transform spawnTransform)
    {
        InstantiateBallWithImpulse(id-1, spawnTransform, Vector3.left);
        InstantiateBallWithImpulse(id-1, spawnTransform, Vector3.right);
    }

    // This method creates a ball with the given parameters at a specific transform with a direction for impulse
    void InstantiateBallWithImpulse(int id, Transform spawnTransform, Vector3 direction)
    {
        GameObject ball;
        ball = Instantiate(ballPrefabs[id], spawnTransform.position, ballPrefabs[id].transform.rotation);
        ball.GetComponent<BallScript>().InitializeBall(this);
        ball.GetComponent<Rigidbody>().AddForce(direction * 3, ForceMode.Impulse);
    }
}
