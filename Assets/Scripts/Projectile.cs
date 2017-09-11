using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Projectile : MonoBehaviour {

    //The direction and speed the projectile is moving
    public Vector3 direction;
    public float speed;
    public float destructiondistance;
    public float damage;

    //The object that shot the projectile
    public GameObject shooter;

    //Important gameobejcts to keep track of
    private GameObject player;
    private float visibility;
    private float tileSize;

	// Use this for initialization
	void Start () {

        //Find specific game objects and set them relativly in this script
        player = GameObject.FindGameObjectWithTag("Player");
        visibility = GameObject.FindGameObjectWithTag("MapGen").GetComponent<CreateGrid>().visibleDistance;
        tileSize = GameObject.FindGameObjectWithTag("MapGen").GetComponent<CreateGrid>().tileSize;

        //Set the starting size of the bullet to be invisible
        transform.localScale = new Vector3(0, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {

        //Move the bullet
        //Calulate the size of the bullet
        transform.position += direction * speed * Time.deltaTime;
        ProjectileSize(Vector3.Distance(player.transform.position, transform.position));

        //If the object that shot doesn't exist destroy the projectile
        if (shooter == null)
        {
            Destroy(gameObject);
        }

        //Calculate the distance from the shooter
        float distance = Vector3.Distance(shooter.transform.position, transform.position);

        //If the bullet is farther than the distance destroy the bullet
        if (distance >= destructiondistance)
        {
            Destroy(gameObject);
        }
	}

    /// <summary>
    /// Run when the bullet runs into another object
    /// </summary>
    /// <param name="col">The object the bullet collided into</param>
    void OnCollisionEnter(Collision col)
    {
        //Test if the bullet hits a destroyable object
        //Destroy the object
        if (col.transform.tag != "Player" &&
            col.transform.tag != "Tile" &&
            tag != "Enemy")
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Calculate the size of the bullet
    /// </summary>
    /// <param name="distance">The distance from the player to the bullet</param>
    private void ProjectileSize(float distance)
    {
        //The modified size of the bullet (aka max size)
        float setSize = .5f;

        //If the bullet is outside the visible range
        //Calulate the size of the bullet
        //If the bullet size is less than 0, set the size to 0
        if (distance > tileSize * visibility)
        {
            distance -= tileSize * visibility;
            setSize = setSize * ((tileSize - distance) / tileSize);

            if (setSize <= 0)
            {
                setSize = 0;
            }
        }

        //Set the scale of the bullet
        transform.localScale = new Vector3(setSize, setSize, setSize);
    }
}
