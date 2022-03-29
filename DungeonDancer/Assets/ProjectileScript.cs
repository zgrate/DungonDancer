using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{

    public Vector3 direction;
    public Vector3 startPosition;
    public bool playerProjectile;
    public float speed;
    public float directionMultiplier = 2;


    public void EnableCollider()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = startPosition + direction* directionMultiplier;
        gameObject.GetComponent<Rigidbody2D>().velocity = direction * speed;
        if ((Vector2)direction == Vector2.up)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 90);
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if ((Vector2)direction == Vector2.down)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 90);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if ((Vector2)direction == Vector2.right)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if ((Vector2)direction == Vector2.left)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerProjectile)
        {
            if (collision.tag == "Enemy")
            {
                Destroy(gameObject);
                Destroy(collision.gameObject);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
