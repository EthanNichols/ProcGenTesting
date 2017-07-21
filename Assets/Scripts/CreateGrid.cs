using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class CreateGrid : MonoBehaviour
{

    //Minimum and maximum view distance
    //Current visible distance
    public int minDistance;
    public int maxDistance;
    public int visibleDistance;

    //Whether to change the view distance or not
    //View distance change positive or negative
    public bool changeViewDistance;
    private bool positiveChange = true;

    //The size of tiles on the map
    //The tile to spawn for the ground
    //The parent for all the tiles
    public int tileSize;
    public GameObject tile;
    public GameObject map;

    //The seed for the map
    public int seed;

    //The player
    private GameObject player;
    public List<Vector3> activatedTraps = new List<Vector3>();

    // Use this for initialization
    void Start()
    {

        //Determine the starting visible range
        visibleDistance = Random.Range(minDistance, maxDistance);

        //Set the player
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        //The last position of the map generator
        //Calculate the new position of the map generator relative to the player position
        Vector3 lastpos = transform.position;
        Vector2 playerPos = PlayerGridPos();
        transform.position = new Vector3(playerPos.x, 0, playerPos.y);

        //Test if the map generator is in a new position
        if (lastpos != transform.position)
        {
            GenerateTiles();

            //Test if the view distance can change
            //Change the view distance
            if (changeViewDistance)
            {
                ChangeView();
            }
        }
    }

    private void GenerateTiles()
    {
        //Create a list of all the tiles that currently exist
        List<GameObject> tiles = GameObject.FindGameObjectsWithTag("Tile").ToList();

        //Loop through the visible size distance
        for (int x = -visibleDistance - 1; x <= visibleDistance + 1; x++)
        {
            for (int y = -visibleDistance - 1; y <= visibleDistance + 1; y++)
            {
                //Whether a tile exists or not
                bool exists = false;

                //Loop through all of the tiles that currently exist
                foreach (GameObject tile in tiles)
                {
                    //Test if the position of a new tiles already exists
                    if (new Vector3(transform.position.x + x * tileSize, 0, transform.position.z + y * tileSize) == tile.transform.position)
                    {
                        exists = true;
                    }
                }

                //If there is no tile in the position create a new tile
                if (!exists)
                {
                    //Instantiate the tile
                    //Set the size of the tile
                    //Put the tile in the empty map object for organization
                    GameObject newTile = Instantiate(tile, new Vector3(transform.position.x + x * tileSize, 0, transform.position.z + y * tileSize), Quaternion.identity);
                    newTile.transform.localScale = new Vector3(tileSize, .1f, tileSize);
                    newTile.transform.parent = map.transform;
                    newTile.tag = "Tile";

                    //Pass the size, visible distance and map seed to the tile
                    newTile.GetComponent<TileAppearing>().tileSize = tileSize;
                    newTile.GetComponent<TileAppearing>().visibility = visibleDistance;
                    newTile.GetComponent<TileAppearing>().seed = seed;

                    //Calculate the perlin color of the tile for the map
                    newTile.GetComponent<Renderer>().material.color = CalcColor((int)((transform.position.x + x * tileSize) / tileSize), (int)((transform.position.z + y * tileSize) / tileSize));
                }
            }
        }
    }

    private void ChangeView()
    {
        //Test the direction the view distance is changing
        //Change the view distance respectivly
        if (positiveChange)
        {
            visibleDistance++;
        }
        else
        {
            visibleDistance--;
        }

        //Make sure the view distance stays in the range of the max and min
        if (visibleDistance <= minDistance)
        {
            positiveChange = true;
        }
        else if (visibleDistance >= maxDistance)
        {
            positiveChange = false;
        }
    }

    private Vector2 PlayerGridPos()
    {
        //Get the closest grid positon for the player
        int x = (int)(Mathf.RoundToInt(player.transform.position.x / tileSize) * tileSize);
        int y = (int)(Mathf.RoundToInt(player.transform.position.z / tileSize) * tileSize);

        //Return the grid position of the player
        return new Vector2(x, y);
    }

    private Color CalcColor(int x, int y)
    {
        //Calculate the map land
        float grey = Mathf.PerlinNoise((seed + x) / 50f, (seed + y) / 50f);
        return new Color(grey, grey, grey);
    }
}
