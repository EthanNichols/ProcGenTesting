using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour {

    public int maxHealth;
    public int maxMana;

    public GameObject healthBar;
    public GameObject manaBar;

    private int currentHealth;
    private int currentMana;

	// Use this for initialization
	void Start () {
        currentHealth = maxHealth;
        currentMana = maxMana;
	}
	
	// Update is called once per frame
	void Update () {
        healthBar.transform.GetChild(1).GetComponent<Text>().text = currentHealth + "/" + maxHealth;
        manaBar.transform.GetChild(1).GetComponent<Text>().text = currentMana + "/" + maxMana;

        healthBar.transform.GetChild(0).transform.localScale = new Vector3(currentHealth / (float)maxHealth, 1, 1);
        manaBar.transform.GetChild(0).transform.localScale = new Vector3(currentMana / (float)maxMana, 1, 1);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
        if (currentMana <= 0)
        {
            currentMana = 0;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Enemy")
        {
            currentHealth--;

            if (col.transform.GetComponent<Projectile>() != null)
            {
                Destroy(col.gameObject);
            }
        }
    }
}
