using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour {


    GameObject [] weapons;
    public GameObject[] enemies;
	// Use this for initialization
	void Start () {
        //GameObject []bigs=GameObject
	}
	
	// Update is called once per frame
	void Update () {
        weapons = GameObject.FindGameObjectsWithTag("Weapon");
	}

    public GameObject[] getWeapons()
    {
        weapons = GameObject.FindGameObjectsWithTag("Weapon");
        return weapons;
    }
}
