using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpecialTile : MonoBehaviour
{

    private Vector2 specialTilePos;
    private int roomSize;
    private int tileSize;

    private GameObject player;
    public bool active;

    // Use this for initialization
    void Start()
    {
        specialTilePos = new Vector2(transform.position.x, transform.position.z);
        roomSize = 10;
        tileSize = GetComponent<TileAppearing>().tileSize;

        player = GameObject.FindGameObjectWithTag("Player");

        GetComponent<TileAppearing>().removeTile = false;
        active = true;
        GetComponent<Renderer>().material.color = new Color(1, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

        Deactivate();

        if (active)
        {
            CreateRoom();
        }
    }

    private void CreateRoom()
    {
        List<GameObject> tiles = GameObject.FindGameObjectsWithTag("Tile").ToList();

        foreach (GameObject tile in tiles)
        {
            bool wall = true;

            if (tile.transform.position.x >= specialTilePos.x - roomSize * tileSize &&
                tile.transform.position.x <= specialTilePos.x + roomSize * tileSize &&
                tile.transform.position.z >= specialTilePos.y - roomSize * tileSize &&
                tile.transform.position.z <= specialTilePos.y + roomSize * tileSize)
            {
                wall = false;
            }

            if (wall)
            {
                if (tile.GetComponent<Floor>() != null)
                {
                    tile.AddComponent<Wall>();
                    Destroy(tile.GetComponent<Floor>());
                }
            }
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

    private void Deactivate()
    {
        Vector2 d1 = new Vector2(player.transform.position.x, player.transform.position.z);

        float distance = Vector2.Distance(d1, specialTilePos);

        if (distance <= tileSize)
        {
            GetComponent<TileAppearing>().removeTile = true;
            active = false;
        }
    }
}
