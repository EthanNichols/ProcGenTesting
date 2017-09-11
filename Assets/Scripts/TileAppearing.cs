using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileAppearing : MonoBehaviour
{

    //The size and visibility range of the tile
    //Whether the tile can be destroyed or not
    //The seed of the map
    public int tileSize;
    public int visibility;
    public bool removeTile;
    public int seed;

    //Enemy that can be spawned
    public GameObject enemy;

    //The player
    //The material color of the tile
    private GameObject player;
    private Color materialColor;

    // Use this for initialization
    void Start()
    {
        //List of traps that have been activated
        List<Vector3> activatedTraps = GameObject.FindGameObjectWithTag("MapGen").GetComponent<CreateGrid>().activatedTraps;

        //Set the player
        //Make the tile size 0 since it isn't visible when it spawns
        player = GameObject.FindGameObjectWithTag("Player");
        transform.localScale = new Vector3(0, .1f, 0);

        //Whether the tile is special or not
        bool special = false;

        //Make sure the tile is a shade of grey
        if (GetComponent<Renderer>().material.color.r == GetComponent<Renderer>().material.color.b &&
            GetComponent<Renderer>().material.color.r == GetComponent<Renderer>().material.color.g)
        {

            //Test for a specific shade of grey
            if (GetComponent<Renderer>().material.color.r >= .5 &&
                GetComponent<Renderer>().material.color.r <= .5002)
            {
                //Make the tile special
                //Add the floor component to the tile
                special = true;
                gameObject.AddComponent<Floor>();

                //Make sure there is only 1 special tile activated at a time
                //Make sure the special tile hasn't been activated yet
                if (GameObject.FindGameObjectsWithTag("SpecialTile").Count() == 0 &&
                    !activatedTraps.Contains(transform.position))
                {
                    //Add all the components to make it a special tile
                    //Add the tile to the activated tile list
                    gameObject.AddComponent<SpecialTile>();
                    tag = "SpecialTile";
                    GameObject.FindGameObjectWithTag("MapGen").GetComponent<CreateGrid>().activatedTraps.Add(transform.position);
                }
            }

            if (!special)
            {
                //Determine which tiles are walls and floor tiles
                if (GetComponent<Renderer>().material.color.r < .4 ||
                    GetComponent<Renderer>().material.color.r > .6)
                {
                    gameObject.AddComponent<Wall>();

                } else {
                    gameObject.AddComponent<Floor>();
                    gameObject.GetComponent<Floor>().enemy = enemy;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        //Get the position of the player and the tile
        Vector2 d1 = new Vector2(player.transform.position.x, player.transform.position.z);
        Vector2 d2 = new Vector2(transform.position.x, transform.position.z);

        //Calculate the distance between the player and the tile
        float distance = Vector2.Distance(d1, d2);

        //If the tile can be removed test if it will be deleted
        if (removeTile)
        {
            DeleteTile(distance);
        }
    }

    /// <summary>
    /// //If the tile is farther than the visible range then delete the tile
    /// </summary>
    /// <param name="distance">The distance from the player to the tile</param>
    private void DeleteTile(float distance)
    {
        if (distance >= tileSize * (visibility + 2))
        {
            Destroy(gameObject);
        }
    }
}
