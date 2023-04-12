using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatEnemyAI : MonoBehaviour
{
    GameObject player;
    public bool patrol = true, guard = false, clockwise = false;
    public bool moving = true;
    public bool pursuingPlayer = false, goingToLastLoc = false;
    Vector3 target;
    Rigidbody2D rig;
    public Vector3 playerLastPos;
    RaycastHit2D hit;
    float speed = 5.0f;
    int layerMask = 1 << 8;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerLastPos = this.transform.position;
        rig = this.GetComponent<Rigidbody2D>();
        layerMask = ~layerMask;
        //this.GetComponent<Animator>().SetBool("moving", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerStats>().dead==false)
        {
            movement();
            playerDetect();
        }
    }
    void movement()
    {
        float distance = Vector3.Distance(player.transform.position, this.transform.position);

        Vector3 direction = player.transform.position - transform.position;

        hit = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(direction.x, direction.y), distance, layerMask);

        Debug.DrawRay(transform.position, direction, Color.red);

        Vector3 fwt = this.transform.TransformDirection(Vector3.right);

        RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(fwt.x, fwt.y), 1.0f, layerMask);

        Debug.DrawRay(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(fwt.x, fwt.y), Color.cyan);

        if (moving == true)
        {

            if (Vector3.Distance(this.transform.position, player.transform.position) >1)
            {

                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }

        }
        if (patrol == true)
        {
            speed = 4.0f;

            if (hit2.collider != null)
            {
                if (hit2.collider.gameObject.tag == "Wall")
                {
                    //Quaternion rot = this.transform.rotation;
                    if (clockwise == false)
                    {
                        transform.Rotate(0, 0, 90);
                    }
                    else
                    {
                        transform.Rotate(0, 0, -90);
                    }
                }
            }
        }
        if (pursuingPlayer == true)
        {
            speed = 7.0f;
            rig.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((playerLastPos.y - transform.position.y), (playerLastPos.x - transform.position.x)) * Mathf.Rad2Deg);
            if (hit.collider.gameObject.tag == "Player")
            {
                playerLastPos = player.transform.position;
            }
        }

        if (goingToLastLoc == true)
        {
            speed = 5.0f;
            //transform.Translate(Vector3.right * 4 * Time.deltaTime);
            rig.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((playerLastPos.y - transform.position.y), (playerLastPos.x - transform.position.x)) * Mathf.Rad2Deg);

            if (Vector3.Distance(this.transform.position, playerLastPos) < 1.5f)
            {
                patrol = true;
                goingToLastLoc = false;
            }
        }

    }


    void playerDetect()
    {
        Vector3 pos = this.transform.InverseTransformPoint(player.transform.position);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Player" && Vector3.Distance(this.transform.position, player.transform.position) <16)
            {
                patrol = false;
                pursuingPlayer = true;
            }
            else
            {
                if (pursuingPlayer == true)
                {
                    goingToLastLoc = true;
                    pursuingPlayer = false;
                }
            }
        }
    }
}
