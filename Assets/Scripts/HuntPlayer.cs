using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code structure from:
// https://github.com/Brackeys/Homing-Missile


public class HuntPlayer : MonoBehaviour
{

    public Transform target;
    public float speed;
    public float rotationSpeed;

    private Rigidbody2D rb;

    public AudioSource boing;
    public AudioSource zap;

    private bool huntMode;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        huntMode = false;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = (Vector2)target.position - rb.position;
        float distance = direction.magnitude;
        HuntModeCheck(distance);
        direction.Normalize();

        float rotateValue;
        Vector3 orientation;
        

        if (huntMode) orientation = -transform.up;
        else orientation = transform.up;
        
        rotateValue = Vector3.Cross(direction, orientation).z;

        rb.angularVelocity = -rotateValue * rotationSpeed;

        rb.velocity = direction * speed;
    }

    void HuntModeCheck(float distance)
    {
        if (huntMode && distance > 4)
        {
            huntMode = false;
        }
        else if (distance <= 4)
        {
            huntMode = true;
        }
    }
}
