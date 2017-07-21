using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    public float shootingDelay;
    public GameObject projectile;

    private Plane plane = new Plane(Vector3.up, Vector3.zero);
    private float lastShot;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButton(0) &&
            lastShot >= shootingDelay)
        {

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float ent = 100f;
            if (plane.Raycast(ray, out ent))
            {
                var hitPoint = ray.GetPoint(ent);

                Vector3 norm = Vector3.Normalize(new Vector3(hitPoint.x - transform.position.x, 0, hitPoint.z - transform.position.z));

                var proj = Instantiate(projectile, new Vector3(transform.position.x + norm.x, .6f, transform.position.z + norm.z), Quaternion.identity);
                proj.GetComponent<Projectile>().direction = norm;
                proj.GetComponent<Projectile>().speed = 50;
                proj.GetComponent<Projectile>().destructionTime = .2f;
            }

            lastShot = 0;
        }

        lastShot += Time.deltaTime;
	}
}
