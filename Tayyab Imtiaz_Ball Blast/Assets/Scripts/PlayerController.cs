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

    void PlayerMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * Time.deltaTime * playerSpeed * horizontalInput);
    }

    void ShootFire()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            shootPosition = new Vector3(transform.position.x, transform.position.y + 1f, 0);
            Instantiate(firePrefab, shootPosition, firePrefab.transform.rotation);
        }     
    }

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

    public void IncrementScore()
    {
        score++;
        DisplayScore();
    }

    void DisplayScore()
    {
        Debug.Log("Score: " + score);
    }

    void DisplayHealth()
    {
        Debug.Log("Health: " + health);
    }

    public void GetDamage()
    {
        health--;
        DisplayHealth();
        CheckGameOver();
    }

    void CheckGameOver()
    {
        if(health < 1)
        {
            gameOver = true;
            Debug.Log("Player Score: " + score + " !!! Game Over !!!");
        }
    }

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
