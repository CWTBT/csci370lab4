using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStinger : MonoBehaviour
{
    PolygonCollider2D stingerPoly;
    public AudioSource zapSound;
    // Start is called before the first frame update
    void Start()
    {
        stingerPoly = gameObject.GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is CapsuleCollider2D && collision.IsTouching(stingerPoly))
        {
            /* FILL IN PLAYER HEALTH/LIVES LOGIC HERE
             * 
             */ 
            GameManager.Instance.DecHealth();
            zapSound.Play();
        }
    }
}
