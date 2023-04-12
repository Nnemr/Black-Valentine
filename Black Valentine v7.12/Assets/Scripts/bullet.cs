using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

    public Vector3 direction;
    string creator;
    EnemyAttacked enemy;
    FatEnemyAttacked fatEnemy;
    public float muzzleVelocity;
    public GameObject bloodImpact;
    float timer = 10.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	
    void Update () {
        transform.Translate(direction * (muzzleVelocity) * Time.deltaTime);
        timer -= Time.deltaTime;
        if(timer<=0)
        {
            Destroy(this.gameObject);
        }
	}

    public void setVals(Vector3 dir,string name,float muzzle)
    {
        direction = dir;
        creator = name;
        muzzleVelocity = muzzle;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemy = other.gameObject.GetComponent<EnemyAttacked>();
            enemy.killFirearm();
            Instantiate(bloodImpact, this.transform.position, this.transform.rotation);
            muzzleVelocity = 0;
            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag=="Big")
        {
            fatEnemy = other.gameObject.GetComponent<FatEnemyAttacked>();
            fatEnemy.gotShot();
        }
        else if (other.gameObject.tag == "Wall")
        {
            Debug.Log("wall");
            muzzleVelocity = 0;
            Destroy(this.gameObject);
        }
        else { }
    }
}
