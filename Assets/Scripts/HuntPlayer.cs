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
    private CapsuleCollider2D topCollider;
    private PolygonCollider2D stingerCollider;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        topCollider = GetComponent<CapsuleCollider2D>();
        stingerCollider = GetComponent<PolygonCollider2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = (Vector2)target.position - rb.position;

        direction.Normalize();

        float rotateValue = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = -rotateValue * rotationSpeed;

        rb.velocity = transform.up * speed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        {

            //https://answers.unity.com/questions/188775/having-more-than-one-collider-in-a-gameobject.html
            if (collision.GetComponent<Collider>().GetType() == typeof(CapsuleCollider2D))
            {
                // do stuff only for the top of jellyfish
            }
            else if (collision.GetComponent<Collider>().GetType() == typeof(PolygonCollider2D))
            {
                // do stuff only for the bottom of jellyfish
            }
        }
    }
}
