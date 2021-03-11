using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stinger : MonoBehaviour
{

    BoxCollider2D stingerBox;
    public AudioSource zapSound;
    // Start is called before the first frame update
    void Start()
    {
        stingerBox = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is CapsuleCollider2D && collision.IsTouching(stingerBox))
        {
            Destroy(collision.gameObject);
            GameManager.Instance.DecFishCount();
            zapSound.Play();
        }
    }
}
