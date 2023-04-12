using UnityEngine;
using System.Collections;

public class camerafollowplayer : MonoBehaviour
{

    GameObject player;
    bool followPlayer = true;
    public float x;
    public float y;
    public float z;
    public KeyCode zoom;
  
    Camera cam;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        cam = Camera.main;
        zoom = KeyCode.LeftShift;
    }

    public void setFollowPlayer(bool val)
    {
        //Relevant in other scripts in progress.
        followPlayer = val;
    }

    public void camFollowPlayer()
    {
        //the function simply dertermines where the player is and changes the position of the camera accordingly 

        x = player.transform.position.x;
        y = player.transform.position.y;
        z = this.transform.position.z;


        //by practical experiment we have to use vector3 in order to keep the camera on the same plane as the player 
        Vector3 newPos = new Vector3(x, y, z);
        this.transform.position = (newPos);
    }
    void zoomforward()
    {
        //Determines the distance between the mouse and player and gives the mean of the mean as boundry to movement.
        //Boundry is too tight and uncontrollable.
        Vector2 mousePos;
        Vector2 playerPos;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerPos = player.transform.position;
        Vector2 mean = (playerPos + mousePos) / 2f;
        transform.position = new Vector3(((playerPos.x + mean.x) / 2f), ((playerPos.y + mean.y) / 2f), -10);

        //Same idea but using the mean instead as boundry
        //Boundry too broad and still unctrollable.
        /*Vector2 mousePos;
        Vector2 playerPos;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerPos = player.transform.position;
        Vector2 c = (playerPos + mousePos) / 2f;
        transform.position=new Vector3(((playerPos.x + mousePos.x) / 2f),((playerPos.y + mousePos.y) / 2f),-10);*/


        //fixes the position of the camera as where the mouse is.
        //Has no boundries and using condition to limit it is disastorous.
        /*Vector3 camPos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
        camPos.z = -10;
        Vector3 dir = camPos - this.transform.position;
        float limit=2;
        if(player.GetComponent<SpriteRenderer>().isVisible)
        transform.Translate(dir * 2 * Time.deltaTime);
        else
        {
            dir.x -= limit;
            dir.y -= limit;
            transform.Translate(dir * 2 * Time.deltaTime);
        }*/
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(zoom))
        {
            followPlayer = false;
        }
        else
        {
            followPlayer = true;
        }

        if (followPlayer == true)
        {
            camFollowPlayer();
        }
        else
        {
            zoomforward();
        }
    }
    float lengthdir_x(float len, Vector3 dir)
    {
        dir = dir.normalized * len;
        return dir.x;
    }
    float lengthdir_y(float len, Vector3 dir)
    {
        dir = dir.normalized * len;
        return dir.y;
    }
}
