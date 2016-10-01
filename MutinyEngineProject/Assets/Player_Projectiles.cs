using UnityEngine;
using System.Collections;

public class Player_Projectiles : MonoBehaviour
{
    public GameObject projectile;
    private Stall currentStall;
    public Transform ProjectileSpawn;
    public Transform ProjectileSpawnLeft;
    public Transform ProjectileSpawnRight;
    private bool canPickup = false;

    public int Player_Ammo;
    public bool Projectile_triple;
    public float powTime;

    private float pickUpCooldown;

    private int playerNumber;
    private string submit;

    // Use this for initialization
    void Start()
    {
        playerNumber = GetComponent<PlayerMovement>().playerNumber;
        submit = string.Format("P{0} Attack", playerNumber);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canPickup)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetButtonDown(submit))
            {
                if (currentStall != null)
                {
                    var tempAmmo = 0;
                    var tempProjectile = currentStall.GiveUpTheGoods(out tempAmmo);
                    if(tempAmmo != 0 && tempProjectile != null)
                    {
                        projectile = tempProjectile;
                        Player_Ammo = tempAmmo;
                    }
                    else
                    {
                        ShootProjectile();
                    }
                }
                else
                {
                    ShootProjectile();
                }
            }
        }
        else
        {
            if ((Input.GetKeyDown(KeyCode.Mouse0) || (Input.GetButtonDown(submit)))&&(Player_Ammo > 0))
            {
                ShootProjectile();
            }
        }
        if (Projectile_triple == true)
        {
            powTime -= Time.deltaTime;
            if (powTime < 0)
            {
                Projectile_triple = false;
            }
        }
    }

    void ShootProjectile()
    {
        if (projectile != null)
        {
            if (Projectile_triple == true)
            {
                Instantiate(projectile, ProjectileSpawn.position, ProjectileSpawn.rotation);
                Instantiate(projectile, ProjectileSpawnLeft.position, ProjectileSpawnLeft.rotation);
                Instantiate(projectile, ProjectileSpawnRight.position, ProjectileSpawnRight.rotation);
                Player_Ammo = Player_Ammo - 3;
            }
            else
            {
                Instantiate(projectile, ProjectileSpawn.position, ProjectileSpawn.rotation);
                Player_Ammo = Player_Ammo - 1;
            }
        }
        
        if(Player_Ammo <= 0)
        {
            projectile = null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "StallTrigger")
        {
            Debug.Log("stall");
            canPickup = true;
            currentStall = other.GetComponentInParent<Stall>();
        }
        if (other.gameObject.tag == "Cake_Powerup")
        {
            Projectile_triple = true;
            Destroy(other.gameObject);
            powTime = other.GetComponentInParent<Powerup_triple>().Powerup_time;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "StallTrigger")
        {
            canPickup = false;
            currentStall = null;
        }
    }
}
