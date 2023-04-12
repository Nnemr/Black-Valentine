using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowScript : MonoBehaviour {

    public Sprite broken;
    public SpriteRenderer spriteControl;
    public BoxCollider2D window;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Weapon")
        {
            spriteControl.sprite = broken;
            window.isTrigger = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            window.isTrigger = false;
    }
}
