using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public Vector3 direction;
    public float speed;
    public float destructionTime;
    private float timeAlive;

	// Use this for initialization
	void Start () {
        timeAlive = 0;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += direction * speed * Time.deltaTime;

        timeAlive += Time.deltaTime;

        if (timeAlive >= destructionTime)
        {
            Destroy(gameObject);
        }
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
