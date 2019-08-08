using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    public int hp = 50;
	// Use this for initialization
	void Start () {
		
	}
	
    public void DamageWall (int amount) {
        hp -= amount;
        if (hp <= 0)
            Destroy(gameObject);
    }
}
