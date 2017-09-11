using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    //The amount of time between shots, the last time shot, the distance projectiles can move
    public float shootingDelay;
    private float lastShot;
    public float shootingDistance;

    //The prohectile
    public GameObject projectile;

    //Invisble plane the player click to calulate the shooting vector
    private Plane plane = new Plane(Vector3.up, Vector3.zero);

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        //Test if the player is shooting, and if a new projectile can be shot
        if (Input.GetMouseButton(0) &&
            lastShot >= shootingDelay)
        {
            //Cast a raw to the position where the mouse is, the distance from the camera
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float ent = 100f;

            //Make sure the mouse is on the plane
            if (plane.Raycast(ray, out ent))
            {
                //Get the position the mouse is on the plane
                var hitPoint = ray.GetPoint(ent);

                //Get the direction the mouse is relative to the player
                //Instantiate the projectile a small distance from the player in the correct direction
                Vector3 norm = Vector3.Normalize(new Vector3(hitPoint.x - transform.position.x, 0, hitPoint.z - transform.position.z));
                var proj = Instantiate(projectile, new Vector3(transform.position.x + norm.x, .6f, transform.position.z + norm.z), Quaternion.identity);

                //Set who shot the projectile, the direction the projectile is going, the speed, and the distance it can travel
                proj.GetComponent<Projectile>().shooter = gameObject;
                proj.GetComponent<Projectile>().direction = norm;
                proj.GetComponent<Projectile>().speed = 50;
                proj.GetComponent<Projectile>().destructiondistance = shootingDistance;
            }

            //Reset the last time the player shot
            lastShot = 0;
        }

        //Increase the amount of time since the player last shot
        lastShot += Time.deltaTime;
	}
}
