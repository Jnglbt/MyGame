using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageHealth : MonoBehaviour {

    //public static int TARGET_BOULDER = 0;
    //public int health, type;
    public int health;

	void Start () {
        /*if (type == TARGET_BOULDER) health = 20;
        else health = 50;*/
	}
	
	// Update is called once per frame
	void Update () {

    }

    public virtual void gotHit(int damage)
    {
        health -= damage;
        if (health <= 0) Die();
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
