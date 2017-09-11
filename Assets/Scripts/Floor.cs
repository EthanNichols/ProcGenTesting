using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

    //The size of the tiles, the player, and the visibility the player has
    private int tileSize;
    private GameObject player;
    private int TileVisibility;

    //Enemy that will be instantiated
    public GameObject enemy;

    // Use this for initialization
    void Start () {

        //Set the tilesize, player, and visibility
        tileSize = GetComponent<TileAppearing>().tileSize;
        player = GameObject.FindGameObjectWithTag("Player");
        TileVisibility = GetComponent<TileAppearing>().visibility;

        //Color the tile a random shade of grey
        float grey = Random.value;
        while (grey < .5f ||
               grey > .6f)
        {
            grey = Random.value;
        }
        GetComponent<Renderer>().material.color = new Color(grey, grey, grey);

        //Randomly spawn enemies on tiles that are a floor
        int spawnEnemy = (int)Random.Range(0, 300);
        if (spawnEnemy == 1 &&
            GetComponent<Floor>() != null &&
            enemy != null)
        {
            GameObject spawnedEnemy = Instantiate(enemy, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity);
            spawnedEnemy.transform.position = new Vector3(spawnedEnemy.transform.position.x, spawnedEnemy.transform.localScale.y / 2 + .1f, spawnedEnemy.transform.position.z);
            spawnedEnemy.GetComponent<Enemy>().speed = 10;
        }
    }
	
	// Update is called once per frame
	void Update () {

        float setSize = tileSize;

        //Get the position of the player and the tile
        Vector2 d1 = new Vector2(player.transform.position.x, player.transform.position.z);
        Vector2 d2 = new Vector2(transform.position.x, transform.position.z);

        //Calculate the distance between the player and the tile
        float distance = Vector2.Distance(d1, d2);

        //If the tile is farther than the view distance calculate the size
        if (distance > tileSize * TileVisibility)
        {

            //Calculate the size, if it is less than 0 set the size to 0
            distance -= tileSize * TileVisibility;
            setSize = tileSize - distance;

            if (setSize <= 0)
            {
                setSize = 0;
            }
        }

        //Set the size of the tile
        transform.localScale = new Vector3(setSize, .1f, setSize);
    }
}
