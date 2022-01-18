using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject firePrefab;
    public float horizontalInput;
    public float playerSpeed;
    public float leftRightBounds;
    public int score;
    public int health;
    public bool gameOver;
    private Vector3 shootPosition;
    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        DisplayScore();
        DisplayHealth();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        ShootFire();
        KeepPlayerInbounds();
    }

    // This method controls the movement of the ball on the x-axis
    void PlayerMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * Time.deltaTime * playerSpeed * horizontalInput);
    }

    // This method instantiates a fire prefab in the scene whenever "Space" key is pressed
    void ShootFire()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            shootPosition = new Vector3(transform.position.x, transform.position.y + 1f, 0);
            Instantiate(firePrefab, shootPosition, firePrefab.transform.rotation);
        }     
    }

    // This method keeps the player inside the playable area
    void KeepPlayerInbounds()
    {
        if(transform.position.x < -leftRightBounds)
        {
            transform.position = new Vector3(-leftRightBounds, transform.position.y, 0);
        } else if (transform.position.x > leftRightBounds)
        {
            transform.position = new Vector3(leftRightBounds, transform.position.y, 0);
        }
    }

    // This method increases the score and uses another method to display it every time it is called
    public void IncrementScore()
    {
        score++;
        DisplayScore();
    }

    // This method prints the score on the console
    void DisplayScore()
    {
        Debug.Log("Score: " + score);
    }

    // This method prints the health on the console
    void DisplayHealth()
    {
        Debug.Log("Health: " + health);
    }

    // This method decrements the health, then displays it, and checks if the game is over
    public void GetDamage()
    {
        health--;
        DisplayHealth();
        CheckGameOver();
    }

    // This method checks if the game is over when player has 0 health
    void CheckGameOver()
    {
        if(health < 1)
        {
            gameOver = true;
            Debug.Log("Player Score: " + score + " !!! Game Over !!!");
        }
    }


    // This method destroys game objects and calls other logic of balls instantiation on collision
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("BigBall"))
        {
            collision.gameObject.GetComponent<BallScript>().InstantiateSmallBalls();
            Destroy(collision.gameObject);
            GetDamage();
        }
        if (collision.gameObject.CompareTag("SmallBall"))
        {
            Destroy(collision.gameObject);
            GetDamage();
        }
    }
}
