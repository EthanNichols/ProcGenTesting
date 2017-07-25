using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int speed;
    public GameObject projectile;

    private GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        int shoot = Random.Range(0, 100);

        Debug.Log(shoot);

        if (shoot < 60)
        {
            gameObject.AddComponent<EnemyShoot>();
        }
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance >= 8 &&
            gameObject.GetComponent<EnemyShoot>() != null)
        {
            GetComponent<Rigidbody>().position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }
}
