using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {

    public float shootingTimer;
    public float shootingDistance;

    private GameObject projectile;
    private GameObject player;
    private float lastShot;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        projectile = GetComponent<Enemy>().projectile;

        if (shootingTimer == 0)
        {
            shootingTimer = 1;
        }

        if (shootingDistance == 0)
        {
            shootingDistance = 11;
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (lastShot >= shootingTimer)
        {
            Vector3 norm = Vector3.Normalize(new Vector3(player.transform.position.x - transform.position.x, 0, player.transform.position.z - transform.position.z));
            GameObject proj = Instantiate(projectile, new Vector3(transform.position.x + norm.x * transform.localScale.x, .6f, transform.position.z + norm.z * transform.localScale.y), Quaternion.identity);

            proj.tag = "Enemy";
            proj.GetComponent<Projectile>().shooter = gameObject;
            proj.GetComponent<Projectile>().direction = norm;
            proj.GetComponent<Projectile>().speed = 50;
            proj.GetComponent<Projectile>().destructiondistance = shootingDistance;

            lastShot = 0;
        }

        lastShot += Time.deltaTime;
	}
}
