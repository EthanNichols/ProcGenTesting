using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    //The size of the tile, the player, and if the tile is visible
    private int tileSize;
    private GameObject player;
    private int TileVisibility;

	// Use this for initialization
	void Start () {

        //Get information that is needed for the script
        tileSize = GetComponent<TileAppearing>().tileSize;
        player = GameObject.FindGameObjectWithTag("Player");
        TileVisibility = GetComponent<TileAppearing>().visibility;

        //Set a random grey color for the wall
        float grey = Random.value / 20;
        GetComponent<Renderer>().material.color = new Color(grey, grey, grey);
    }
	
	// Update is called once per frame
	void Update () {

        //The maximum size of the tile
        float setSize = tileSize;

        //Get the position of the player and the tile
        Vector2 d1 = new Vector2(player.transform.position.x, player.transform.position.z);
        Vector2 d2 = new Vector2(transform.position.x, transform.position.z);

        //Calculate the distance between the player and the tile
        float distance = Vector2.Distance(d1, d2);

        //Calculate the size of the tile
        //If the tile size is below 0, set the size to 0
        if (distance > tileSize * TileVisibility)
        {
            distance -= tileSize * TileVisibility;
            setSize = tileSize - distance;

            if (setSize <= 0)
            {
                setSize = 0;
            }
        }

        //Set the scale of the object
        transform.localScale = new Vector3(setSize, 5, setSize);
    }
}
