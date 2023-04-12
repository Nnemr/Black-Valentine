using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatEnemyAttacked : MonoBehaviour
{

    public bool EnemyKnockedDown = false, TakeDown = false;
    float TakeDowntimer = 0.0f;
    float knockDownTimer = 3.0f;
    public GameObject player, BloodSplatter;
    SpriteRenderer spriteControl;
    public Sprite bulletDeath;
    public Animator anim;
    public int health = 4;
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

    }
    public void gotShot()
    {
        health--;
        if(health<=0){
           killFirearm();
        }
    }
   
    public void killFirearm()
    {
        player.GetComponent<PlayerStats>().addScore(250);
        //anim.enabled = false;
        spriteControl.sprite = bulletDeath;
        spriteControl.sortingOrder = 2;
        this.GetComponent<Collider2D>().enabled = false;
        Instantiate(BloodSplatter, this.transform.position, this.transform.rotation);
        this.gameObject.tag = "Dead";
        this.GetComponent<FatEnemyAI>().enabled = false;
        this.GetComponent<FatEnemyWeapons>().enabled = false;
        this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        //Destroy(feet);

    }

}
