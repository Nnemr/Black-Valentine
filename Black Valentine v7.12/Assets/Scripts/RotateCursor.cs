using UnityEngine;
using System.Collections;

//The script make the sprite rotate with the postion of the mouse 

public class RotateCursor : MonoBehaviour {

	Vector2 mousepos;
	Vector2 direction;
	Rigidbody2D rid;
	Camera cam;
	// Use this for initialization
	void Start () {
        //First we establish the object we want rotated, which is the player or whatever object that has the script
		rid = this.GetComponent<Rigidbody2D> ();

        //Then we establish the main camera as a script object 
		cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        // the function call
		rotateCamera ();
	}
	void rotateCamera()
	{

        //The following three line determine the position of the mouse based on the location in the camera view
		mousepos=cam.ScreenToWorldPoint(new Vector2(Input.mousePosition.x,Input.mousePosition.y));
        float ypos = (mousepos.y - transform.position.y);
        float xpos = (mousepos.x - transform.position.x);
        //As demonstrated by experiment done by me, we can't use vector2 in this method
        //It will cause the sprite to turn around itself without taking in consideration the Z axis
        //So instead of the sprite rotating in 2d format, it rotates in 3d format and doesn't follow the mouse

        //The rigid body object the changes it's angle using the tangent of the previously made x y positions
        rid.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(ypos, xpos))*Mathf.Rad2Deg;
	}
}
