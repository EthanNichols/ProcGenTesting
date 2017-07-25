using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public Vector3 direction;
    public float speed;
    public float destructiondistance;
    public float damage;

    public GameObject shooter;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += direction * speed * Time.deltaTime;

        if (shooter == null)
        {
            Destroy(gameObject);
        }

        float distance = Vector3.Distance(shooter.transform.position, transform.position);

        if (distance >= destructiondistance)
        {
            Destroy(gameObject);
        }
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag != "Player" &&
            col.transform.tag != "Tile" &&
            tag != "Enemy")
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
}
