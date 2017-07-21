using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Speed of the player
    public float speed;

    private Rigidbody selfRigibody;

    // Use this for initialization
    void Start()
    {
        //Set the rigidbody of the player
        selfRigibody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //Test for the key directions being pressed
        if (Input.GetKey(KeyCode.W))
        {
            vertical = 1;
        } else if (Input.GetKey(KeyCode.S))
        {
            vertical = -1;
        } else
        {
            vertical = 0;
        }

        if (Input.GetKey(KeyCode.A))
        {
            horizontal = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontal = 1;
        }
        else
        {
            horizontal = 0;
        }

        //Move the player in the right direction
        Vector3 movement = new Vector3(horizontal * speed, 0f, vertical * speed);
        selfRigibody.velocity = movement;
    }
}
