using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour {

    //The max health for the player
    public int maxHealth;
    public int maxMana;

    //The health bar
    public GameObject healthBar;
    public GameObject manaBar;

    //The current health for the player
    private int currentHealth;
    private int currentMana;

	// Use this for initialization
	void Start () {
        //Set the starting health
        currentHealth = maxHealth;
        currentMana = maxMana;
	}
	
	// Update is called once per frame
	void Update () {

        //Display the amount of health the player has
        healthBar.transform.GetChild(1).GetComponent<Text>().text = currentHealth + " / " + maxHealth;
        manaBar.transform.GetChild(1).GetComponent<Text>().text = currentMana + " / " + maxMana;

        //Diaplay the amount of health relative to the max health
        healthBar.transform.GetChild(0).transform.localScale = new Vector3(currentHealth / (float)maxHealth, 1, 1);
        manaBar.transform.GetChild(0).transform.localScale = new Vector3(currentMana / (float)maxMana, 1, 1);

        //Make sure the health doesn't go below 0
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
        if (currentMana <= 0)
        {
            currentMana = 0;
        }
    }

    /// <summary>
    /// Run this function when the player collides with something
    /// </summary>
    /// <param name="col"></param>
    void OnCollisionEnter(Collision col)
    {
        //Test if the player collided with an enemy
        if (col.transform.tag == "Enemy")
        {

            //Decrease the amount of health the player has
            currentHealth--;

            //Destroy the object if it is a projectile
            if (col.transform.GetComponent<Projectile>() != null)
            {
                Destroy(col.gameObject);
            }
        }
    }
}
