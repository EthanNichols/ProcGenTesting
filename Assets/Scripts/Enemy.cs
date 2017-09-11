using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //The speed of the enemy
    //The project the enemy shoots
    public int speed;
    public GameObject projectile;

    //The player, visibility of the player, and the size of the tiles
    private GameObject player;
    private float visibility;
    private float tileSize;

    // Use this for initialization
    void Start()
    {
        //Set the player, visibility, and tilesize for the game
        player = GameObject.FindGameObjectWithTag("Player");
        visibility = GameObject.FindGameObjectWithTag("MapGen").GetComponent<CreateGrid>().maxDistance;
        tileSize = GameObject.FindGameObjectWithTag("MapGen").GetComponent<CreateGrid>().tileSize;

        //Randomly allow some enemies to shoot
        int shoot = Random.Range(0, 100);
        if (shoot < 60)
        {
            gameObject.AddComponent<EnemyShoot>();
        }

        //Set the scale since enemies generally spawn out of visibility range
        transform.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Calulate the distance the enemy is from the player
        //Whether the enemy should move or not
        float distance = Vector3.Distance(transform.position, player.transform.position);
        bool move = false;

        //Destroy the enemy if it is too far from the player
        //Calculate the size the enemy is
        ToFarFromPlayer(distance);
        EnemySize(distance);

        //If the player doesn't shoot, the enemy always moves
        if (gameObject.GetComponent<EnemyShoot>() == null)
        {
            move = true;
        }

        //If the player does shoot, stop moving when it's in range of the player
        else if (distance >= 8 &&
            gameObject.GetComponent<EnemyShoot>() != null)
        {
            move = true;
        }

        //Move the enemy towards the player
        if (move)
        {
            GetComponent<Rigidbody>().position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Destroy the enemy if it is to far from the player
    /// </summary>
    /// <param name="distance">The distance the enemy is to the player</param>
    private void ToFarFromPlayer(float distance)
    {
        //Test the distance the enemy is from the player, and make sure there isn't a special room activated
        //Destroy the enemy, since it is too far
        if (distance >= (visibility + 5) * 3 &&
            GameObject.FindGameObjectWithTag("SpecialTile") == null)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Calculate the size the enemy is depending on the visibilty
    /// </summary>
    /// <param name="distance">The distance the enemy is to the player</param>
    private void EnemySize(float distance)
    {
        //The maximum size of the enemy
        float setSize = 1.5f;

        //If the enemy is out of the player visibility
        if (distance > tileSize * visibility)
        {
            //Calculate the size the enemy should be
            //If the enemy size is less than 0, set the size to 0
            distance -= tileSize * visibility;
            setSize = setSize * ((tileSize - distance) / tileSize);

            if (setSize <= 0)
            {
                setSize = 0;
            }
        }

        //Set the size of the enemy
        transform.localScale = new Vector3(setSize, setSize, setSize);
    }
}
