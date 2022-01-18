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

    void FireMovement()
    {
        transform.Translate(Vector3.up * Time.deltaTime * fireSpeed);
    }

    void DestroyOutOfBounds()
    {
        if(transform.position.y > upBound)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<BallScript>() != null)
        {
            other.gameObject.GetComponent<BallScript>().GotFired();
            Destroy(gameObject);
        }
    }
}
