using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowWeapon : MonoBehaviour
{
    EnemyAttacked enemy;
    public float timer = 1.0f;
    Vector3 direction;
    Rigidbody2D rig;
    GameObject player;
    // Use this for initialization
    void Start()
    {
        Debug.Log("Initialize Throwing");
        player = GameObject.FindGameObjectWithTag("Player");
        rig = this.GetComponent<Rigidbody2D>();
        rig.AddForce(direction * 200);
        Debug.Log("Force is 150");
    }

    // Update is called once per frame
    void Update()
    {

        transform.rotation = Quaternion.Slerp(this.transform.rotation, new Quaternion(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z - 1, this.transform.rotation.w), Time.deltaTime * 8);
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            rig.velocity = Vector3.zero;
            Debug.Log("Done");
            Destroy(this);
        }
    }
    public void setDirection(Vector3 dir)
    {
        direction = dir;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemy = other.gameObject.GetComponent<EnemyAttacked>();
            enemy.knockDown();
            rig.velocity = Vector3.zero;
            Debug.Log("Thrown At enemy");
            enemy.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            Destroy(this);
            Debug.Log("Destroyed");
        }
        else if(other.gameObject.tag == "Weapon")
        {

        }
        else if (other.gameObject.tag != "Player" && other.gameObject.tag != "Melee")
        {
            rig.velocity = Vector3.zero;
            Destroy(this);
        }
        this.GetComponent<BoxCollider2D>().enabled = false;
    }
}
