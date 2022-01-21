using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    public float fireSpeed;
    public float upBound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FireMovement();
        DestroyOutOfBounds();
    }

    // This method moves the fire GameObject upwards
    void FireMovement()
    {
        transform.Translate(Vector3.up * Time.deltaTime * fireSpeed);
    }

    // This method destroys the fire GameObject when it is outside the upper bounds
    void DestroyOutOfBounds()
    {
        if(transform.position.y > upBound)
        {
            Destroy(gameObject);
        }
    }

    // This method calls the scripts in Ball GameObject on Collision
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ball"))
        {
            other.gameObject.GetComponent<BallScript>().BallIsHit();
            Destroy(gameObject);
        }
    }
}
