using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;
    float vertical;

    public float moveSpeed = 5f;
    private float moveLimiter = 0.7f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

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
        body.AddForce(new Vector2(horizontal * moveSpeed, vertical * moveSpeed));

    }
}
