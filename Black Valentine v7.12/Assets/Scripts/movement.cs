using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour
{

    public bool moving = false;
    public bool IsFacingRight;
    private bool IsFacingLeft;
    public bool IsFacingUp;
    public bool IsFacingDown;
    public KeyCode W;
    public KeyCode D;
    public KeyCode S;
    public KeyCode A;
    public Animator anim;
    public float speed = 5.0f;
    private float x, y, z;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        x = transform.localScale.x;
        y = transform.localScale.y;
        x = transform.localScale.z;
        IsFacingRight = true;
        IsFacingUp = false;
        IsFacingLeft = false;
        IsFacingDown = false;
    }


    void move()
    {
        //whether to use vector2 or vector3 at this point is completely irrelevant 

        if (Input.GetKey(W))
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime, Space.World);
            //transform.Translate(Vector3.up*speed*Time.deltaTime,Space.World);
            moving = true;
            if (!IsFacingUp)
            {
                IsFacingUp = true;
                if (IsFacingRight)
                {
                    transform.Rotate(0, 0, 90);
                    IsFacingRight = false;
                }
                else if (IsFacingLeft)
                {
                    transform.Rotate(0, 0, 270);
                    IsFacingLeft = false;
                }
                else if (IsFacingDown)
                {
                    transform.Rotate(0, 0, 180);
                    IsFacingDown = false;
                }

            }
        }
        if (Input.GetKey(S))
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime, Space.World);
            //transform.Translate(Vector3.down*speed*Time.deltaTime,Space.World);
            moving = true;
            if (!IsFacingDown)
            {
                IsFacingDown = true;
                if (IsFacingRight)
                {
                    transform.Rotate(0, 0, 270);
                    IsFacingRight = false;
                }
                else if (IsFacingLeft)
                {
                    transform.Rotate(0, 0, 90);
                    IsFacingLeft = false;
                }
                else if (IsFacingUp)
                {
                    transform.Rotate(0, 0, 180);
                    IsFacingUp = false;
                }
            }
        }
        if (Input.GetKey(A))
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime, Space.World);
            //transform.Translate(Vector3.left*speed*Time.deltaTime,Space.World);
            moving = true;
            if (!IsFacingLeft)
            {
                IsFacingLeft = true;
                if (IsFacingRight)
                {
                    transform.Rotate(0, 0, 180);
                    IsFacingRight = false;
                }
                else if (IsFacingUp)
                {
                    transform.Rotate(0, 0, 90);
                    IsFacingUp = false;
                }
                else if (IsFacingDown)
                {
                    transform.Rotate(0, 0, 270);
                    IsFacingDown = false;
                }

            }

        }
        if (Input.GetKey(D))
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime, Space.World);
            //transform.Translate (Vector3.right * speed * Time.deltaTime, Space.World);
            moving = true;

            if (!IsFacingRight)
            {
                IsFacingRight = true;
                if (IsFacingLeft)
                {
                    transform.Rotate(0, 0, 180);
                    IsFacingLeft = false;
                }
                else if (IsFacingUp)
                {
                    transform.Rotate(0, 0, 270);
                    IsFacingUp = false;
                }
                else if (IsFacingDown)
                {
                    transform.Rotate(0, 0, 90);
                    IsFacingDown = false;
                }
            }
        }
        if (Input.GetKey(D) != true
            && Input.GetKey(A) != true
            && Input.GetKey(S) != true
            && Input.GetKey(W) != true)
            moving = false;

        anim.SetBool("moving",moving);
        //this condition is relevant in another script in progress.

    }

    // Update is called once per frame
    void Update()
    {

        move();
    }
}
