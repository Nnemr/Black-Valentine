using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacked : MonoBehaviour
{

    public bool EnemyKnockedDown = false, TakeDown = false;
    float TakeDowntimer = 0.0f;
    public GameObject feet;
    float knockDownTimer = 3.0f;
    public GameObject player, BloodSplatter;
    SpriteRenderer spriteControl;
    public Sprite bulletDeath, meleeDeath, knockedDownSprite, backUp;
    public Animator anim;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spriteControl = this.GetComponent<SpriteRenderer>();
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyKnockedDown == true)
        {
            knockDown();
        }
       

    }
    public void knockDown()
    {
        knockDownTimer -= Time.deltaTime;
        this.GetComponent<Collider2D>().isTrigger = true;
        spriteControl.sprite = knockedDownSprite;
        spriteControl.sortingOrder = 3;
        EnemyKnockedDown = true;
        this.GetComponent<BoxCollider2D>().size = new Vector2(0.6148422f, this.GetComponent<BoxCollider2D>().size.y);
        anim.SetBool("moving", false);
        anim.enabled = false;
        /*if(this.GetComponent<AIPathFinding>()!=null)
        {
            this.GetComponent<AIPathFinding>().enabled = false;
        }*/
        if (knockDownTimer <= 0)
        {
            EnemyKnockedDown = false;
            spriteControl.sprite = backUp;
            anim.enabled = true;
            this.GetComponent<Collider2D>().isTrigger = false;
            anim.SetBool("moving", true);
            this.GetComponent<EnemyAI>().enabled = true;
            spriteControl.sortingOrder = 4;
            knockDownTimer = 3.0f;
            this.GetComponent<BoxCollider2D>().size = new Vector2(0.1256528f, this.GetComponent<BoxCollider2D>().size.y);
            return;
        }
        this.GetComponent<EnemyAI>().enabled = false;
    }
    public void killFirearm()
    {
        player.GetComponent<PlayerStats>().addScore(250);
        anim.enabled = false;
        spriteControl.sprite = bulletDeath;
        spriteControl.sortingOrder = 2;
        this.GetComponent<Collider2D>().enabled = false;
        Instantiate(BloodSplatter, this.transform.position, this.transform.rotation);
        this.gameObject.tag = "Dead";
        this.GetComponent<EnemyAI>().enabled = false;
        this.GetComponent<EnemyWeapons>().dropWeapon();
        
        this.GetComponent<EnemyWeapons>().enabled = false;
        Destroy(feet);
        /*if (this.GetComponent<AIPathFinding>() != null)
        {
            this.GetComponent<AIPathFinding>().enabled = false;
        }*/

    }
    public void killMelee()
    {
        player.GetComponent<PlayerStats>().addScore(250);
        anim.enabled = false;
        spriteControl.sprite = meleeDeath;
        spriteControl.sortingOrder = 2;
        this.GetComponent<Collider2D>().enabled = false;
        Instantiate(BloodSplatter, this.transform.position, this.transform.rotation);
        this.gameObject.tag = "Dead";
        this.GetComponent<EnemyAI>().enabled = false;
        this.GetComponent<EnemyWeapons>().dropWeapon();
        this.GetComponent<EnemyWeapons>().enabled=false;
        Destroy(feet);
       /* if (this.GetComponent<AIPathFinding>() != null)
        {
            this.GetComponent<AIPathFinding>().enabled = false;
        }*/
    }

   
    public void TakeDownAction()
    {
        player.GetComponent<PlayerStats>().addScore(500);
        spriteControl.sprite = meleeDeath;
        knockDownTimer = 5.0f;
        Debug.Log("TakeDown");
        anim.enabled = false;
       /* if (this.GetComponent<AIPathFinding>() != null)
        {
            this.GetComponent<AIPathFinding>().enabled = false;
        }*/
        this.GetComponent<EnemyWeapons>().dropWeapon();
        this.GetComponent<EnemyWeapons>().enabled = false;
        this.GetComponent<EnemyAI>().enabled = false;
        this.GetComponent<Collider2D>().enabled = false;
        Destroy(feet);
        Instantiate(BloodSplatter, this.transform.position, this.transform.rotation);
        spriteControl.sortingOrder = 1;
        this.gameObject.tag = "Dead";
        this.enabled = false;
    }
}
