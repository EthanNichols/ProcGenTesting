using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpecialTile : MonoBehaviour
{
    //The position, and size of the special room, and the size of the tiles
    private Vector2 specialTilePos;
    private int roomSize;
    private int tileSize;

    private GameObject player;

    //Whether the room is currently activated
    public bool active;

    // Use this for initialization
    void Start()
    {
        //Set the position, size of the room, and the size of the tiles
        specialTilePos = new Vector2(transform.position.x, transform.position.z);
        roomSize = 10;
        tileSize = GetComponent<TileAppearing>().tileSize;

        //Set the player object
        player = GameObject.FindGameObjectWithTag("Player");

        //Make sure the tile can't be destroyed, the room is active
        GetComponent<TileAppearing>().removeTile = false;
        active = true;

        //TEMPERARY
        //Change the color of the tile
        GetComponent<Renderer>().material.color = new Color(1, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Test if the player deactivated the room
        Deactivate();

        //While the room is active effect the tiles of the room
        if (active)
        {
            CreateRoom();
        }
    }

    /// <summary>
    /// Effect which blocks are tiles, and walls
    /// This makes the room, and also ensures the player doesn't trap themself
    /// </summary>
    private void CreateRoom()
    {
        //Get a list of the current tiles
        List<GameObject> tiles = GameObject.FindGameObjectsWithTag("Tile").ToList();

        //Go through all of the tiles
        foreach (GameObject tile in tiles)
        {

            //Start all the tiles as walls
            bool wall = true;

            //If the tile is within the size of the room, make the tile a floor tile
            if (tile.transform.position.x >= specialTilePos.x - roomSize * tileSize &&
                tile.transform.position.x <= specialTilePos.x + roomSize * tileSize &&
                tile.transform.position.z >= specialTilePos.y - roomSize * tileSize &&
                tile.transform.position.z <= specialTilePos.y + roomSize * tileSize)
            {
                wall = false;
            }

            //If the tile is a wall remove the floor component, and add the wall component
            if (wall)
            {
                if (tile.GetComponent<Floor>() != null)
                {
                    tile.AddComponent<Wall>();
                    Destroy(tile.GetComponent<Floor>());
                }
            }

            //Remove the wall component, and add the floor component
            else
            {
                if (tile.GetComponent<Wall>() != null)
                {
                    tile.AddComponent<Floor>();
                    Destroy(tile.GetComponent<Wall>());
                }
            }
        }
    }

    /// <summary>
    /// Test if the room is deactivated by the player
    /// </summary>
    private void Deactivate()
    {
        //The player's position on a 2d plane
        Vector2 d1 = new Vector2(player.transform.position.x, player.transform.position.z);

        //Calculate the distance between the tile and the player
        float distance = Vector2.Distance(d1, specialTilePos);

        //If the player is on the tile deactivate the room, and allow the tile to be destroyed
        if (distance <= tileSize)
        {
            GetComponent<TileAppearing>().removeTile = true;
            active = false;
        }
    }
}
