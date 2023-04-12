using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Weapons : MonoBehaviour
{
    public weaponPickup WeaponUsed;
    public bullet Bullet;
    public int bulletNo;
    public GameObject oneHandedSpawnArea, twoHandedSpawnArea,shotgunShell;
    public bool firearm = false;
    public float timer = 0.1f, timerReset = 0.1f;
    public Animator anim;
    public int meleeCounter;
    public float weaponchangeTime = 0.5f;
    public bool changingWeapon = false;
    // Use this for initialization
    void Start()
    {
        anim = this.GetComponent<Animator>();
        WeaponUsed = null;
        meleeCounter = 0;
    }
    public AudioClip[] shootClips;
    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;

        }
        if (Input.GetMouseButton(0) && timer <= 0)
        {
            attack();
        }
        if (Input.GetMouseButtonDown(1) && changingWeapon == false)
        {
            Debug.Log("Dropping");
            dropWeapon();
        }
        if (changingWeapon == true)
        {
            weaponchangeTime -= Time.deltaTime;
            if (weaponchangeTime <= 0)
            {
                changingWeapon = false;
            }
        }
    }
    public int getCurrentBullets()
    {
        if (WeaponUsed == null)
            return 0;
        return WeaponUsed.ammo;
    }

    public void setWeapon(weaponPickup current, string name, float rateOfFire, bool gun, bool oneHanded)
    {
        changingWeapon = true;
        WeaponUsed = current;

        if (WeaponUsed != null)
        {
            string weaponType = "carrying" + name;
            Debug.Log(weaponType);
            anim.SetBool(weaponType, true);
            anim.SetBool("weapons", true);
        }
        else
        {
            anim.SetBool("carryingPistol", false);
            anim.SetBool("carryingRevolver", false);
            anim.SetBool("carryingTommy", false);
            anim.SetBool("carryingBatton", false);
            anim.SetBool("carryingShotgun", false);
            anim.SetBool("weapons", false);
        }

        firearm = gun;

        timerReset = rateOfFire;
        timer = timerReset;
    }

    public weaponPickup getWeaponUsed()
    {
        return WeaponUsed;
    }

    public void dropWeapon()
    {
        if (WeaponUsed != null)
        {
            Vector3 mousPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            WeaponUsed.gameObject.AddComponent<ThrowWeapon>();
            Vector3 dir;

            dir.x = mousPos.x - this.transform.position.x;
            dir.y = mousPos.y - this.transform.position.y;
            dir.z = 0;

            WeaponUsed.GetComponent<Rigidbody2D>().isKinematic = false;
            WeaponUsed.GetComponent<ThrowWeapon>().setDirection(dir);
            WeaponUsed.transform.position = oneHandedSpawnArea.transform.position;
            WeaponUsed.transform.eulerAngles = this.transform.eulerAngles;
            WeaponUsed.GetComponent<ThrowWeapon>().enabled = true;
            WeaponUsed.gameObject.SetActive(true);
            WeaponUsed.GetComponent<BoxCollider2D>().enabled = true;
            setWeapon(null, "", 0.5f, false, false);
        }
    }

    public void attack()
    {
        if (firearm == true && WeaponUsed.ammo >0)
        {
            string shoot = "shoot";
            --WeaponUsed.ammo;
            bullet projectile = Bullet.GetComponent<bullet>();
            Vector3 dir;
            dir.x = Vector2.right.x;
            dir.y = Vector2.right.y;
            dir.z = 0;
            projectile.setVals(dir, "Player", WeaponUsed.muzzleVelocity);
            if (WeaponUsed.onehanded == true)
            {
                Instantiate(Bullet, oneHandedSpawnArea.transform.position, transform.rotation);
            }
            else if(WeaponUsed.shotgun==true)
            {
                Instantiate(shotgunShell, twoHandedSpawnArea.transform.position, transform.rotation);
            }
            else
            {
                Instantiate(Bullet, twoHandedSpawnArea.transform.position, transform.rotation);
            }
            timer = timerReset;
            anim.SetTrigger(shoot);
            AudioManager.instance.playSingle(shootClips[ WeaponUsed.weaponID]);
        }
        else if (firearm == false)
        {
            if (meleeCounter == 0)
            {
                anim.SetBool("left", true);
                
            }
            else
            {
                anim.SetBool("left", false);
            }
            meleeCounter = (++meleeCounter) % 2;

            if (WeaponUsed == null)
            {
                AudioManager.instance.playSingle(shootClips[5]);
                anim.SetTrigger("meleePunch");
                Debug.Log("Test");
            }
            else
            {
                AudioManager.instance.playSingle(shootClips[6]);
                anim.SetTrigger("Hit" + WeaponUsed.Weaponname);
            }
            timer = timerReset;
        }
    }
}
