using UnityEngine;
using System.Collections;

public class Player_Projectiles : MonoBehaviour
{
    public GameObject projectile;
    private GameObject potentialPickup;
    public Transform ProjectileSpawn;
    private bool canPickup = false;

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
                if (potentialPickup != projectile)
                {
                    projectile = potentialPickup;
                }
                else
                {
                    if(projectile != null) Instantiate(projectile, ProjectileSpawn.position, ProjectileSpawn.rotation);
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) || (Input.GetButtonDown(submit)))
            {
                if (projectile != null) Instantiate(projectile, ProjectileSpawn.position, ProjectileSpawn.rotation);
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "StallTrigger")
        {
            Debug.Log("stall");
            canPickup = true;
            potentialPickup = other.GetComponentInParent<Stall>().bakedGood;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "StallTrigger")
        {
            canPickup = false;
        }
    }
}
