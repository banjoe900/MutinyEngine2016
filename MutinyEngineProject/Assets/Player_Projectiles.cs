using UnityEngine;
using System.Collections;

public class Player_Projectiles : MonoBehaviour
{
    PlayerMovement playerMovement;
    public GameObject projectile;
    public string projectileName;
    private Stall currentStall;
    public Transform ProjectileSpawn;
    public Transform ProjectileSpawnLeft;
    public Transform ProjectileSpawnRight;
    public bool canPickup = false;
    //set to false when dead and set to true on new round

    public enum ProjectileWeightClass { Other, Big, Small };

    public int Player_Ammo;
    public bool Projectile_triple;
    public float powTime;

    private float pickUpCooldown;

    private int playerNumber;
    private string submit;

    public GameObject heldCake;
    public GameObject heldCookie;
    public GameObject heldCroissant;
    public GameObject[] Cakes;
    GameObject Cakes_Projectile;
    public GameObject[] Croissants;
    GameObject Croissants_Projectile;
    public GameObject[] Cookies;
    GameObject Cookies_Projectile;
    public GameObject[] Pies;
    GameObject Pies_Projectile;
    int index;
    int frame;

    private CharacterAudioManager audioManager;

    // Use this for initialization
    void Start()
    {
        //heldCake.SetActive(false);
        //heldCookie.SetActive(false);
        //heldCroissant.SetActive(false);
        playerMovement = GetComponent<PlayerMovement>();
        playerNumber = playerMovement.playerNumber;
        audioManager = GetComponentInChildren<CharacterAudioManager>();

        submit = string.Format("P{0} Attack", playerNumber);




    }
    void Randomizer()
    {
        Random.Range(1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (projectile == null) {
            heldCookie.SetActive(false);
            heldCroissant.SetActive(false);
            heldCake.SetActive(false);
        }
        else
        {
            if (projectileName == "Cake")
            {
                heldCookie.SetActive(false);
                heldCroissant.SetActive(false);
                heldCake.SetActive(true);
            }
            if (projectileName == "Cookie")
            {
                heldCookie.SetActive(true);
                heldCroissant.SetActive(false);
                heldCake.SetActive(false);
            }
            if (projectileName == "Croissant")
            {
                heldCookie.SetActive(false);
                heldCroissant.SetActive(true);
                heldCake.SetActive(false);
            }
        }
        frame++;
        if ((canPickup)&&(Player_Ammo == 0))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetButtonDown(submit))
            {
                if (currentStall != null)
                {
                    var tempAmmo = 0;
                    var tempProjectile = currentStall.GiveUpTheGoods(out tempAmmo);
                    int isPickingUp = 0;
                    if (tempAmmo != 0 && tempProjectile != null)
                    {
                        isPickingUp++;
                        projectile = tempProjectile;
                        Player_Ammo = tempAmmo;
                        audioManager.PlayPickupAudio();
                        Debug.Log("pickup");
                    }
                    if(isPickingUp == 0)
                    {
                        Debug.Log("canPickup and is shooting");
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
            if ((Input.GetKeyDown(KeyCode.Mouse0) || (Input.GetButtonDown(submit))) && (Player_Ammo > 0))
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
            if (projectileName == "Cakes")
            {
                playerMovement.animator.SetTrigger("BigThrow");
                //Sound for cake
                    Debug.Log(Cakes.Length);
                    index = Random.Range(0, Cakes.Length);
                    Cakes_Projectile = Cakes[index];
                    Instantiate(Cakes_Projectile, ProjectileSpawn.position, ProjectileSpawn.rotation);
                    Player_Ammo = Player_Ammo - 1;

            }
            if (projectileName == "Croissants")
            {
                playerMovement.animator.SetTrigger("SmallThrow");
                audioManager.PlayThrowCroissantAudio();
                index = Random.Range(0, Croissants.Length);
                Croissants_Projectile = Croissants[index];
                Instantiate(Croissants_Projectile, ProjectileSpawn.position, ProjectileSpawn.rotation);
                Player_Ammo = Player_Ammo - 1;
            }
            if (projectileName == "Cookies")
            {
                playerMovement.animator.SetTrigger("SmallThrow");
                audioManager.PlayThrowCookieAudio();
                    index = Random.Range(0, Cookies.Length);
                    Cookies_Projectile = Cookies[index];
                    Instantiate(Cookies_Projectile, ProjectileSpawn.position,  ProjectileSpawn.rotation);
                    index = Random.Range(0, Cookies.Length);
                    Cookies_Projectile = Cookies[index];
                    Instantiate(Cookies_Projectile, ProjectileSpawnLeft.position, ProjectileSpawnLeft.rotation);
                    index = Random.Range(0, Cookies.Length);
                    Cookies_Projectile = Cookies[index];
                    Instantiate(Cookies_Projectile, ProjectileSpawnRight.position, ProjectileSpawnRight.rotation);
                    Player_Ammo = Player_Ammo - 1;
                }
            if (projectileName == "Pies")
            {
                playerMovement.animator.SetTrigger("BigThrow");
                //Play pies audio
                if (Projectile_triple == true)
                {
                    index = Random.Range(0, Pies.Length);
                    Pies_Projectile = Pies[index];
                    Instantiate(Pies_Projectile, ProjectileSpawn.position, ProjectileSpawn.rotation);
                    Player_Ammo = Player_Ammo - 1;
                }
            }
        }

        if (Player_Ammo <= 0)
        {
            projectile = null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
    if (Player_Ammo == 0)
        {

        
        if (other.gameObject.tag == "StallTrigger")
        {
            canPickup = true;
            currentStall = other.GetComponentInParent<Stall>();
            projectileName = other.GetComponentInParent<Stall>().goodName;
        }
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

	public int GetPlayerAmmo(){

		return Player_Ammo;

	}


}
