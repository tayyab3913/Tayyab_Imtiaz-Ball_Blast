using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public GameObject smallBallPrefab;
    public float bounceImpulse;
    public int health;
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            Bounce();
        } else if (collision.gameObject.CompareTag("LeftWall"))
        {
            BounceLeft();
        } else if (collision.gameObject.CompareTag("RightWall"))
        {
            BounceRight();
        }
    }

    void Bounce()
    {
        ballRB.AddForce(Vector3.up * bounceImpulse, ForceMode.Impulse);
    }

    void BounceLeft()
    {
        ballRB.AddForce(Vector3.left * 4, ForceMode.Impulse);
    }

    void BounceRight()
    {
        ballRB.AddForce(Vector3.right * 4, ForceMode.Impulse);
    }

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

    public void InstantiateSmallBalls()
    {
        ballReference = Instantiate(smallBallPrefab, transform.position, smallBallPrefab.transform.rotation);
        ballReference.GetComponent<Rigidbody>().AddForce(Vector3.left * 3, ForceMode.Impulse);
        ballReference = Instantiate(smallBallPrefab, transform.position, smallBallPrefab.transform.rotation);
        ballReference.GetComponent<Rigidbody>().AddForce(Vector3.right * 3, ForceMode.Impulse);
    }

    void GetDamageForBigBall()
    {
        health--;
        if(health < 1)
        {
            InstantiateSmallBalls();
            Destroy(gameObject);
        }
    }

    void DestroyOutOfBounds()
    {
        if(transform.position.y < -5)
        {
            DestroyObject(gameObject);
        }
    }
}
