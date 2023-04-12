using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    GameObject player;
    public int spotingDistance;
    public bool patrol = true, guard = false, clockwise = false;
    public bool moving = true, hasGun=false;
    public bool pursuingPlayer = false, goingToLastLoc = false;
    Vector3 target;
    Rigidbody2D rig;
    public Vector3 playerLastPos;
    RaycastHit2D hit;
    float speed = 5.0f;
    int layerMask = 1 << 8;
    ObjectManager objManager;
    GameObject[] weapons;
    EnemyWeapons enemyWeaponsController;
    public GameObject WeaponToGoTo;
    public bool goingToWeapon = false;

    GameObject lastPos;
    public GameObject lastPosMarker;
   // public AIPathFinding aip;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerLastPos = this.transform.position;
        objManager = GameObject.FindGameObjectWithTag("GameControlle").GetComponent<ObjectManager>();
        rig = this.GetComponent<Rigidbody2D>();
        layerMask = ~layerMask;
        enemyWeaponsController = this.GetComponent<EnemyWeapons>();
        this.GetComponent<Animator>().SetBool("moving", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (guard == true)
        {
            if (pursuingPlayer == true) { moving = true; guard = false; }
            else
                moving = false;
        }
        if (player.GetComponent<PlayerStats>().dead == false)
        {
            movement();
            playerDetect();
            canEnemyFindWeapon();
        }
       // disableAstar();
    }
    void movement()
    {
        float distance = Vector3.Distance(player.transform.position, this.transform.position);
        
        Vector3 direction = player.transform.position - transform.position;
        
        hit = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(direction.x, direction.y), distance, layerMask);
        
        Debug.DrawRay(transform.position, direction, Color.red);

        Vector3 fwt = this.transform.TransformDirection(Vector3.right);

        RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(fwt.x, fwt.y), 4.0f, layerMask);
        
        Debug.DrawRay(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(fwt.x, fwt.y), Color.cyan);
        
        if (moving == true)
        {
            if (hasGun == false)
            { 
                transform.Translate(Vector3.right * speed * Time.deltaTime); 
            }
            else
            {
                if(Vector3.Distance(this.transform.position,player.transform.position)<15 && pursuingPlayer==true)
                {

                }
                else
                {
                    transform.Translate(Vector3.right * speed * Time.deltaTime);
                }
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
            if(WeaponToGoTo!=null)
            {
                patrol = false;
                goingToWeapon = true;
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
                //disableAstar();
            }
        }

        if (goingToWeapon == true)
        {
            speed = 6.0f;
            rig.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((WeaponToGoTo.transform.position.y - transform.position.y), (WeaponToGoTo.transform.position.x - transform.position.x)) * Mathf.Rad2Deg);
            if (enemyWeaponsController.getWeaponUsed() != null)
            {
                WeaponToGoTo = null;
                patrol = true;
                goingToWeapon = false;
                pursuingPlayer = false;
                goingToLastLoc = false;
            }
            if (WeaponToGoTo == null)
            {
                WeaponToGoTo = null;
                patrol = true;
                goingToWeapon = false;
                pursuingPlayer = false;
                goingToLastLoc = false;
            }
        }
    }

   /* public void heardGunshot(Vector3 lastPositionVec)
    {
        if(Vector3.Distance(this.transform.position,player.transform.position)<10.0f)
        {
            playerLastPos = lastPositionVec;
            goingToLastLoc = true;
            if(aip==null)
            {
               // aip = gameObject.AddComponent<AIPathFinding>();
                lastPos = (GameObject)Instantiate(lastPosMarker, playerLastPos, this.transform.rotation);
                aip.target = lastPos.transform;
            }
            else
            {
            }
        }
    }*/

   /* public void disableAstar()
    {
        if(this.GetComponent<AIPathFinding>()==null)
        {

        }
        else
        {
            if (this.GetComponent<AIPathFinding>().enabled==true && goingToLastLoc==false)
            {
              //  Destroy(this.GetComponent<AIPathFinding>());
            }
        }
    }*/
    void setWeaponToGoTo(GameObject weapon)
    {
        WeaponToGoTo = weapon;
        goingToWeapon = true;
        patrol = false;
        pursuingPlayer = false;
        goingToLastLoc = false;
    }

    void canEnemyFindWeapon()
    {
        if(enemyWeaponsController.getWeaponUsed()==null && WeaponToGoTo==null && goingToWeapon==false)
        {

            weapons = objManager.getWeapons();
            for (int x = 0; x < weapons.Length; x++) 
            {
                float distance = Vector3.Distance(this.transform.position, weapons[x].transform.position);
                if(distance<10)
                {
                    Vector3 dir = weapons[x].transform.position - transform.position;
                    RaycastHit2D wepCheck = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(dir.x, dir.y), distance, layerMask);
                    if (wepCheck.collider==null)
                    {

                    }
                    else if(wepCheck.collider.gameObject.tag=="Weapon")
                    {
                        setWeaponToGoTo(weapons[x]);
                    }
                    
                }
            }
        }
    }

    void playerDetect()
    {
        Vector3 pos = this.transform.InverseTransformPoint(player.transform.position);
        if (hit.collider != null)
        {   
            if(hit.collider.gameObject.tag=="Player" && Vector3.Distance(this.transform.position,player.transform.position)<spotingDistance)
            {
                patrol = false;
                pursuingPlayer = true;
                goingToWeapon = false;
            }
            else
            {
                if(pursuingPlayer==true)
                {
                    goingToLastLoc = true;
                   // heardGunshot(playerLastPos);
                    pursuingPlayer = false;
                    goingToWeapon = false;
                }
            }
        }
    }
    public float getSpeed()
    {
        return speed;
    }
}
