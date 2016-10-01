﻿using UnityEngine;
using System.Collections;

public class Player_Projectiles : MonoBehaviour
{
    public GameObject projectile;
    private GameObject potentialPickup;
    public Transform ProjectileSpawn;
    public Transform ProjectileSpawnLeft;
    public Transform ProjectileSpawnRight;
    public int Player_Ammo;
    private bool canPickup = false;
    private bool Projectile_triple = false;

    private int playerNumber;
    private string submit;

    // Use this for initialization
    void Start()
    {
        playerNumber = GetComponent<PlayerMovement>().playerNumber;
        submit = string.Format("P{0} Attack", playerNumber);
        Player_Ammo = 0;
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
                    Instantiate(projectile, ProjectileSpawn.position, ProjectileSpawn.rotation);
                }
            }
        }
        else
        {
            if ((Input.GetKeyDown(KeyCode.Mouse0) || (Input.GetButtonDown(submit)))&&(Player_Ammo > 0))
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
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "StallTrigger")
        {
            Debug.Log("stall");
            canPickup = true;
            potentialPickup = other.GetComponentInParent<Stall>().bakedGood;
            Player_Ammo = other.GetComponentInParent<Stall>().bakedGood_Ammo;
        }
        if (other.gameObject.tag == "Cake_Powerup")
        {
            Projectile_triple = true;
            Destroy(other.gameObject);
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
