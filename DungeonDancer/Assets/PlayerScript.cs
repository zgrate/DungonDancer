using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    public float movementSpeed;
    public InputAction moveAction;

    public Vector2 lastVector;
    public Vector3 direction = Vector2.right;

    public GameObject projectileCloner;
    public float projectileTimeout;
    public bool shoot = false;
    public float shootTime = 1;
    private float lastProjectile;


    private int health = 100, score;
    public int enemyHitCost = 10;
    public Slider healthBar;

    public int coinsOnTheMap;
    private int collectedCoins;

    public GameObject exitObject;
    public GameObject gameOver, gameWon;

    public string nextLevel;


    [Header("Called when the player shoots projectile")]
    public UnityEvent projectileShot;

    public void Shoot()
    {
        var clone = Instantiate(projectileCloner);
        var s = clone.GetComponent<ProjectileScript>();
        s.direction = direction;
        s.startPosition = gameObject.transform.position;
        s.playerProjectile = true;
        clone.SetActive(true);
    }

    public void Fire()
    {
        if(moveAction.enabled && Time.time - lastProjectile > projectileTimeout)
        {
            shoot = true;
            lastProjectile = Time.time;
            projectileShot.Invoke();
        }
    }

    public void EnemyHit()
    {
        health-= enemyHitCost;
    }

    public void CoinCollect()
    {
        score += 100;
        collectedCoins++;
        if(collectedCoins == coinsOnTheMap)
        {
            exitObject.SetActive(true);
        }
    }

    public void AppleCollect()
    {
        health = 100;
    }

    public void ExitCollect()
    {
        //THE END OF LEVEL
        if (nextLevel == "")
        {
            gameWon.SetActive(true);
            moveAction.Disable();
            Time.timeScale = 0;
            isGameOver = true;
        }
        else
        {
            SceneManager.LoadScene(nextLevel);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        moveAction.Enable();
    }

    private bool isGameOver = false;

    // Update is called once per frame
    void Update()
    {
        healthBar.value = health;
        if(health <= 0 && !isGameOver)
        {
            gameOver.SetActive(true);
            moveAction.Disable();
            Time.timeScale = 0;
            isGameOver = true;
        }
        var vector = moveAction.ReadValue<Vector2>();
        var rigid = gameObject.GetComponent<Rigidbody2D>();
        var sprite = gameObject.GetComponent<SpriteRenderer>();
        rigid.velocity = vector * movementSpeed;
        if (shoot)
        {
            GetComponent<Animator>().SetInteger("State", 2);
            shoot = false;
            Invoke("Shoot", shootTime);
        }
        else if (rigid.velocity != Vector2.zero)
        {
            GetComponent<Animator>().SetInteger("State", 1);
        }
        else
        {   
            GetComponent<Animator>().SetInteger("State", 0);
        }

        if(vector.y > 0)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 90);
            sprite.flipX = false;
            direction = Vector2.up;
        }
        else if(vector.y < 0)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 90);
            sprite.flipX = true;
            direction = Vector2.down;
        }
        else if (vector.x > 0)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            sprite.flipX = false;
            direction = Vector2.right;
        }
        else if(vector.x < 0)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            sprite.flipX = true;
            direction = Vector2.left;
        }
         lastVector = vector;
    }
}
