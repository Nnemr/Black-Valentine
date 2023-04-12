using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsCollecter : MonoBehaviour {

	public PlayerStats Player;

	// Use this for initialization

	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.tag == "Player" && Input.GetMouseButtonDown (1)) 
		{
			Debug.Log ("PickedUp");
			Player.GetComponent<PlayerStats> ().addItems(1);
			this.gameObject.SetActive (false);

		}
			
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
