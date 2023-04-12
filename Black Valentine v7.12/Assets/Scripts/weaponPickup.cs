using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponPickup : MonoBehaviour {
    public int ammo, ammoCapacity;
    public string Weaponname;
    public float rateOfFire,muzzleVelocity;
    public bool firearm ,onehanded,shotgun=false;
    public Weapons weaponController;
    public int weaponID;
	// Use this for initialization
	void Start () {

        weaponController = GameObject.FindGameObjectWithTag("Player").GetComponent<Weapons>();
	}
	
	// Update is called once per frame
	void Update () {
        

	}
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag=="Player" && Input.GetMouseButtonDown(1))
        {
            if(weaponController.getWeaponUsed()!=null)
            {
                weaponController.dropWeapon();
            }
            weaponController.setWeapon(this, Weaponname, rateOfFire, firearm,onehanded);
            this.gameObject.SetActive(false);
        }
        else if (other.gameObject.tag=="Enemy" && other.gameObject.GetComponent<EnemyWeapons>().getWeaponUsed()==null)
        {
            EnemyWeapons enemyWeaponController = other.gameObject.GetComponent<EnemyWeapons>();
            enemyWeaponController.setWeapon(this, Weaponname, rateOfFire, firearm, onehanded);
            this.gameObject.SetActive(false);
        }
    }
}
