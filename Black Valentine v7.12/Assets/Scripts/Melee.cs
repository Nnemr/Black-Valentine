using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour {

    EnemyAttacked Enemy;
    Weapons player;
	// Use this for initialization
	void Start () {
        player = gameObject.GetComponentInParent<Weapons>();
        Enemy = null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerStay2D(Collider2D other)
    {
        if((other.tag=="Enemy") && Input.GetMouseButtonDown(0) && player.getWeaponUsed()==null )
        {
            Enemy = other.GetComponent<EnemyAttacked>();
            Enemy.knockDown();
        }
        else if ((other.tag == "Enemy") && Input.GetMouseButtonDown(0))
        {
            Enemy = other.GetComponent<EnemyAttacked>();
            Enemy.killMelee();
        }

        if ((other.tag == "Enemy") && Input.GetKey(KeyCode.Space) && other.isTrigger == true)
        {
            Enemy = other.GetComponent<EnemyAttacked>();
            Enemy.TakeDownAction();
            Enemy.TakeDown = true;
        }
    }
}
