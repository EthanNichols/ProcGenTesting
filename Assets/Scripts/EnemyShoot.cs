using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{

    //The time the enemy has between shots, the last time that was shot, and the distance projectiles can travel
    public float shootingTimer;
    private float lastShot;
    public float shootingDistance;

    //The projectile the enemy 
    private GameObject projectile;
    private GameObject player;

    // Use this for initialization
    void Start()
    {
        //Set the player, and the projectile that is shot
        player = GameObject.FindGameObjectWithTag("Player");
        projectile = GetComponent<Enemy>().projectile;

        //Default shooting speed and distance if they aren't already set
        if (shootingTimer == 0) { shootingTimer = 1; }
        if (shootingDistance == 0) { shootingDistance = 11; }
    }

    // Update is called once per frame
    void Update()
    {
        //Test if the player can shoot again
        if (lastShot >= shootingTimer)
        {
            //Calculate the direction the bullet should be shot from
            //Spawn the projectile a small distance from the enemy
            Vector3 norm = Vector3.Normalize(new Vector3(player.transform.position.x - transform.position.x, 0, player.transform.position.z - transform.position.z));
            GameObject proj = Instantiate(projectile, new Vector3(transform.position.x + norm.x * transform.localScale.x, .6f, transform.position.z + norm.z * transform.localScale.y), Quaternion.identity);

            //Set information about the projecile
            proj.tag = "Enemy";
            proj.GetComponent<Projectile>().shooter = gameObject;
            proj.GetComponent<Projectile>().direction = norm;
            proj.GetComponent<Projectile>().speed = 50;
            proj.GetComponent<Projectile>().destructiondistance = shootingDistance;

            //Reset the last time the enemy shot
            lastShot = 0;
        }

        //Increase the amount of time since the last enemy shot
        lastShot += Time.deltaTime;
    }
}
