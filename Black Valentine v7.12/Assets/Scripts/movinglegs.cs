using UnityEngine;
using System.Collections;

public class movinglegs : MonoBehaviour
{

    public bool moving = false;
    Vector3 rot;
    public KeyCode W;
    public KeyCode D;
    public KeyCode S;
    public KeyCode A;
    public Animator anim;

    public float speed = 5.0f;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rot = new Vector3(0, 0, 0);
    }

    void move()
    {
        //whether to use vector2 or vector3 at this point is completely irrelevant 
        
        if (Input.GetKey(W))
        {
            //transform.Translate(Vector2.up * speed * Time.deltaTime, Space.World);
            moving = true;
           rot = new Vector3(0, 0, 90);
            transform.eulerAngles = rot;
        }

        if (Input.GetKey(S))
        {
            //transform.Translate(Vector2.down * speed * Time.deltaTime, Space.World);
            moving = true;
            rot = new Vector3(0, 0, 270);
            transform.eulerAngles = rot;
        }
        if (Input.GetKey(A))
        {
           // transform.Translate(Vector2.left * speed * Time.deltaTime, Space.World);
            moving = true;
            rot = new Vector3(0, 0, 180);
            transform.eulerAngles = rot;
        }
        if (Input.GetKey(D))
        {

            //transform.Translate(Vector2.right * speed * Time.deltaTime, Space.World);
            moving = true;
            rot = new Vector3(0, 0, 0);
            transform.eulerAngles = rot;
        }

        //this condition is relevant in another script in progress.
        if (Input.GetKey(D) != true
            && Input.GetKey(A) != true
            && Input.GetKey(S) != true
            && Input.GetKey(W) != true)
            moving = false;

        anim.SetBool("moving", moving);
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }
}
