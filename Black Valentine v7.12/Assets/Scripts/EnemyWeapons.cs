using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyWeapons : MonoBehaviour
{
    public GameObject oneHandedSpawnArea, twoHandedSpawnArea, blood,shotgunShell;
    public bool firearm = false;
    public float timer = 0.1f, timerReset = 0.1f;
    public weaponPickup WeaponUsed;
    public bullet Bullet;
    int ammo;

    bool oneHanded = false;
    public float weaponchangeTime = 0.5f;
    public bool changingWeapon = false;

    EnemyAI enemy;
    GameObject player;

    public Animator anim;
    public int meleeCounter;
    // Use this for initialization
    void Start()
    {
        anim = this.GetComponent<Animator>();
        meleeCounter = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = this.GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        enemy.hasGun = firearm;

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if(enemy.hasGun==false && firearm==false &&enemy.pursuingPlayer==true &&timer<=0 &&Vector3.Distance(this.transform.position,player.transform.position)<=4f)
        {
            attack();
        }
        else if(enemy.hasGun==true &&enemy.pursuingPlayer==true &&timer<=0 &&Vector3.Distance(this.transform.position,player.transform.position)<=20.0f)
        {
            attack();
        }
        if (changingWeapon == true)
        {
            weaponchangeTime -= Time.deltaTime;
            if (weaponchangeTime <= 0)
            {
                changingWeapon = false;
            }
        }
        dropWeapon();
    }

    public void setWeapon(weaponPickup current, string name, float rateOfFire, bool gun, bool oneHanded)
    {
        changingWeapon = true;
        WeaponUsed = current;

        if (WeaponUsed != null)
        {
            ammo = WeaponUsed.ammoCapacity;
            string weaponType = "carrying" + name;
            anim.SetBool(weaponType, true);
            anim.SetBool("weapons", true);
        }
        else
        {
            anim.SetBool("carryingPistol", false);
            anim.SetBool("carryingRevolver", false);
            anim.SetBool("carryingBatton", false);
            anim.SetBool("weapons", false);
        }

        firearm = gun;

        timerReset = rateOfFire;
        timer = timerReset;
        this.oneHanded = oneHanded;
    }

    public weaponPickup getWeaponUsed()
    {
        return WeaponUsed;
    }

    public void dropWeapon()
    {
        if ((WeaponUsed != null && (GetComponent<EnemyAttacked>().EnemyKnockedDown==true ||this.gameObject.tag=="Dead")))
        {
            WeaponUsed.transform.position = oneHandedSpawnArea.transform.position;
            WeaponUsed.transform.eulerAngles = this.transform.eulerAngles;
            WeaponUsed.gameObject.SetActive(true);
            setWeapon(null, "", 0.5f, false, false);
        }
    }

    public void attack()
    {
        if (firearm == true && WeaponUsed.ammo > 0)
        {
            string shoot = "shoot";
            --WeaponUsed.ammo;
            if (WeaponUsed.ammo==0)
            {
                WeaponUsed.ammo = ammo;
            }
            bullet projectile = Bullet.GetComponent<bullet>();
            Vector3 dir;
            dir.x = Vector2.right.x;
            dir.y = Vector2.right.y;
            dir.z = 0;
            projectile.setVals(dir, "Enemy", WeaponUsed.muzzleVelocity);
            if (WeaponUsed.onehanded == true)
            {
                Instantiate(Bullet, oneHandedSpawnArea.transform.position, transform.rotation);
            }
            else
            {
                if (WeaponUsed.shotgun == true)
                {
                    Instantiate(shotgunShell, twoHandedSpawnArea.transform.position, transform.rotation);
                }
                else
                {
                    Instantiate(Bullet, twoHandedSpawnArea.transform.position, transform.rotation);
                }
            }
            timer = timerReset;
            anim.SetTrigger(shoot);
        }
        
    }
}
