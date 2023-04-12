using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStats : MonoBehaviour
{
    static int score = 0;
    public int health;
    public GameObject legs;
    public Text scoreUI, ammoUI;
    public Slider h_bar;
    SpriteRenderer death;
    public Sprite playerDeath;
    public int collectedItems = 0;
    public bool dead = false;
    void Start()
    {
        death = this.GetComponent<SpriteRenderer>();
    }
    public void addScore(int sc)
    {
        Debug.Log("Add");
        score += sc;
    }
    public void addItems(int it)
    {
        collectedItems += it;
    }
    public int getScore()
    {
        return score;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bullet")
            takeDamage(50);
    }
    public void takeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            this.GetComponent<Animator>().enabled = false;
            this.GetComponent<movement>().enabled = false;
            this.GetComponentInChildren<movinglegs>().enabled = false;
            this.GetComponent<RotateCursor>().enabled = false;
            death.sprite = playerDeath;
            death.sortingOrder = 1;
            dead = true;
            Application.LoadLevel(5);
        }
    }
    void Update()
    {
        h_bar.value = health;
        ammoUI.text = "Ammo: " + GetComponent<Weapons>().getCurrentBullets();
        scoreUI.text = "Score = " + score;
    }
}

