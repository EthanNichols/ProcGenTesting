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

    //The player
    //The material color of the tile
    private GameObject player;
    private Color materialColor;

    // Use this for initialization
    void Start()
    {

        List<Vector3> activatedTraps = GameObject.FindGameObjectWithTag("MapGen").GetComponent<CreateGrid>().activatedTraps;

        //Set the player
        //Make the tile size 0 since it isn't visible when it spawns
        player = GameObject.FindGameObjectWithTag("Player");
        transform.localScale = new Vector3(0, .1f, 0);

        bool special = false;

        if (GetComponent<Renderer>().material.color.r == GetComponent<Renderer>().material.color.b &&
            GetComponent<Renderer>().material.color.r == GetComponent<Renderer>().material.color.g)
        {

            if (GetComponent<Renderer>().material.color.r >= .5 &&
                GetComponent<Renderer>().material.color.r <= .5002)
            {
                special = true;
                gameObject.AddComponent<Floor>();

                if (GameObject.FindGameObjectsWithTag("SpecialTile").Count() == 0 &&
                    !activatedTraps.Contains(transform.position))
                {
                    gameObject.AddComponent<SpecialTile>();
                    tag = "SpecialTile";
                    GameObject.FindGameObjectWithTag("MapGen").GetComponent<CreateGrid>().activatedTraps.Add(transform.position);
                }
            }

            if (!special)
            {
                if (GetComponent<Renderer>().material.color.r < .4 ||
                    GetComponent<Renderer>().material.color.r > .6)
                {
                    gameObject.AddComponent<Wall>();
                } else {
                    gameObject.AddComponent<Floor>();
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

        //If the tile can be removed test if it willbe deleted
        if (removeTile)
        {
            DeleteTile(distance);
        }
    }

    private void DeleteTile(float distance)
    {
        //If the tile is farther than the visible range then delete the tile
        if (distance >= tileSize * (visibility + 2))
        {
            Destroy(gameObject);
        }
    }
}
