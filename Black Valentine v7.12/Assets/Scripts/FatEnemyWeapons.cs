using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FatEnemyWeapons : MonoBehaviour
{
    public GameObject blood;
    public float timer = 0.1f, timerReset = 0.1f;
   
    bool attacking = false;

    FatEnemyAI enemy;
    GameObject player;

    public Animator anim;
    public int meleeCounter;
    // Use this for initialization
    void Start()
    {
        anim = this.GetComponent<Animator>();
        meleeCounter = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = this.GetComponent<FatEnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if(timer<=0 && Vector3.Distance(this.transform.position,player.transform.position)<=4.0f)
        {
            attack();
        }
     }

 
    public void attack()
    {
        
            /*if (meleeCounter == 0)
            {
                anim.SetBool("left", true);
            }
            else
            {
                anim.SetBool("left", false);
            }*/
            meleeCounter = (++meleeCounter) % 2;

            int layerMask = 1 << 8;
            layerMask = ~layerMask;
            RaycastHit2D ray = Physics2D.Raycast(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), new Vector2(transform.right.x, transform.right.y), 5.0f, layerMask);
            Debug.Log(ray);
            if (ray.collider != null)
            {
                if (ray.collider.gameObject.tag == "Player" || ray.collider.gameObject.tag == "Melee")
                {
                    player.GetComponent<PlayerStats>().takeDamage(1);
                    player.GetComponent<PlayerStats>().dead=true;
                    Instantiate(blood, player.transform.position, player.transform.rotation);
                    //anim.SetTrigger("meleePunch");
                }
            }
            timer = timerReset;
        }
}
