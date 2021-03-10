using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D body;
    Animator animator;

    float horizontal;
    float vertical;
    float speed;

    public float moveSpeed = 5f;
    private float moveLimiter = 0.7f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        speed = body.velocity.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        speed = body.velocity.magnitude;

        animator.SetFloat("horizontal", horizontal);
        animator.SetFloat("vertical", vertical);
        animator.SetFloat("speed", speed);

        if (horizontal < 0)
        {
            this.gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (horizontal > 0)
        {
            this.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }

    }
    private void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0)
        {
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        // old movement if we want to go back
        //body.AddForce(new Vector2(horizontal * moveSpeed, vertical * moveSpeed));

        body.AddForce(body.transform.up * vertical * moveSpeed);
        body.AddTorque(-horizontal);

    }
}
