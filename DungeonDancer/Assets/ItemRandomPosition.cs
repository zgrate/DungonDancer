using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemRandomPosition : MonoBehaviour
{

    public float minX, minY, maxX, maxY;
    
    public Tilemap[] tilesToAvoid;

    public GameObject apple;
    public GameObject coin;
    public GameObject exit;

    public int numberOfApples;
    public int numberOfCoins;

    public GameObject player;
    private List<GameObject> clonedObjects = new List<GameObject>();

    private float distanceTolerance = 1;

    bool CheckCollision(Vector2 position)
    {

        return Physics2D.OverlapBoxAll(position, new Vector2(10, 10), 0).Length != 0;

        foreach (var obj in clonedObjects)
        {
            if (Vector2.Distance(obj.gameObject.transform.position, position) < distanceTolerance)
            {
                return true;
            }
        }
        foreach(var tile in tilesToAvoid)
        {
            if (tile.HasTile(tile.WorldToCell(position)))
            {
                return true;
            }
        }
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        var clonedExit = Instantiate(exit);
        clonedExit.transform.SetParent(transform);
        Debug.Log(clonedExit);
        while (CheckCollision(clonedExit.transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 10)))
        {
            ;
        }
        clonedObjects.Add(clonedExit);
        clonedExit.SetActive(false);
        player.GetComponent<PlayerScript>().coinsOnTheMap = numberOfCoins;
        player.GetComponent<PlayerScript>().exitObject = clonedExit;

        for(int i = 0; i < numberOfApples; i++)
        {
            var clonedApple = Instantiate(apple);
            clonedApple.transform.SetParent(transform);
            while (CheckCollision(clonedApple.transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 10)))
            {
                ;
            }
            clonedObjects.Add(clonedApple);
            clonedApple.SetActive(true);
        }

        for(int i = 0; i < numberOfCoins; i++)
        {
            var clonedCoin = Instantiate(coin);
            clonedCoin.transform.SetParent(transform);
            while (CheckCollision(clonedCoin.transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 10)))
            {
                ;
            }
            clonedObjects.Add(clonedCoin);
            clonedCoin.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
