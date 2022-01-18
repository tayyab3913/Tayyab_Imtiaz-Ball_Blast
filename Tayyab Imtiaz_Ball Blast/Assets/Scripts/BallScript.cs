using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public GameObject smallBallPrefab;
    public float bounceImpulse;
    public int health;
    private int randomImpulse;
    private GameObject ballReference;
    private Rigidbody ballRB;
    private PlayerController playerScript;
    // Start is called before the first frame update
    void Start()
    {
        ballRB = GetComponent<Rigidbody>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        DestroyOutOfBounds();
        if(playerScript.gameOver == true)
        {
            Destroy(gameObject);
        }
    }

    // This method moves the balls based on collisions with different parts of the ground
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            Bounce();
        } else if (collision.gameObject.CompareTag("RightWall"))
        {
            BounceLeft();
        } else if (collision.gameObject.CompareTag("LeftWall"))
        {
            BounceRight();
        }
    }

    // This method bounces the ball when it touches the ground
    // I have also added random physics as an innovation. The balls can move in random directions sometimes
    void Bounce()
    {
        randomImpulse = Random.Range(-1, 2);
        ballRB.AddForce(Vector3.up * bounceImpulse, ForceMode.Impulse);
        ballRB.AddForce(Vector3.right * randomImpulse, ForceMode.Impulse);
    }

    // This method bounces the ball leftwards
    void BounceLeft()
    {
        ballRB.AddForce(Vector3.left * 4, ForceMode.Impulse);
    }

    // This method bounces the ball rightwards
    void BounceRight()
    {
        ballRB.AddForce(Vector3.right * 4, ForceMode.Impulse);
    }

    // This method is called by fire script when it collides with this game object. It increases score and destroys gameobjects
    public void GotFired()
    {
        if(gameObject.CompareTag("BigBall"))
        {
            playerScript.IncrementScore();
            GetDamageForBigBall();
        } else if(gameObject.CompareTag("SmallBall"))
        {
            playerScript.IncrementScore();
            Destroy(gameObject);
        }
    }

    // This method instantiates small balls when the big ball is going to get destroyed
    public void InstantiateSmallBalls()
    {
        ballReference = Instantiate(smallBallPrefab, transform.position, smallBallPrefab.transform.rotation);
        ballReference.GetComponent<Rigidbody>().AddForce(Vector3.left * 3, ForceMode.Impulse);
        ballReference = Instantiate(smallBallPrefab, transform.position, smallBallPrefab.transform.rotation);
        ballReference.GetComponent<Rigidbody>().AddForce(Vector3.right * 3, ForceMode.Impulse);
    }

    // This method decrements damage of the big ball and instantiates small balls when it's health is 0
    void GetDamageForBigBall()
    {
        health--;
        if(health < 1)
        {
            InstantiateSmallBalls();
            Destroy(gameObject);
        }
    }

    // This method destroys the balls in case they fall of the screen
    void DestroyOutOfBounds()
    {
        if(transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }
}
