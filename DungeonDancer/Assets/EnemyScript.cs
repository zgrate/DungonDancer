using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyScript : MonoBehaviour
{
    public float maxDeltaDistance;
    public int target;

    public Vector2 tolerance;
    public Vector3[] points;


    public PlayerScript player;

    // Start is called before the first frame update
    void Start()
    {
 
    }
    
    private void OnGUI()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, points[target], maxDeltaDistance);
        if (Vector2.MoveTowards(transform.position, points[target], maxDeltaDistance) - (Vector2)transform.position == Vector2.zero)
        {
            target++;
            if(target >= points.Length)
            {
                target = 0;
            }
            transform.position = Vector3.MoveTowards(transform.position, points[target], maxDeltaDistance);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.tag == "Player")
        {
            player.EnemyHit();
        }
    }
}
