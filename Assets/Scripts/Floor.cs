using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

    private int tileSize;
    private GameObject player;
    private int TileVisibility;

    // Use this for initialization
    void Start () {
        tileSize = GetComponent<TileAppearing>().tileSize;
        player = GameObject.FindGameObjectWithTag("Player");
        TileVisibility = GetComponent<TileAppearing>().visibility;

        float grey = Random.value;
        while (grey < .5f ||
               grey > .6f)
        {
            grey = Random.value;
        }
        GetComponent<Renderer>().material.color = new Color(grey, grey, grey);
    }
	
	// Update is called once per frame
	void Update () {

        float setSize = tileSize;

        //Get the position of the player and the tile
        Vector2 d1 = new Vector2(player.transform.position.x, player.transform.position.z);
        Vector2 d2 = new Vector2(transform.position.x, transform.position.z);

        //Calculate the distance between the player and the tile
        float distance = Vector2.Distance(d1, d2);

        if (distance > tileSize * TileVisibility)
        {
            distance -= tileSize * TileVisibility;
            setSize = tileSize - distance;

            if (setSize <= 0)
            {
                setSize = 0;
            }
        }

        transform.localScale = new Vector3(setSize, .1f, setSize);
    }
}
