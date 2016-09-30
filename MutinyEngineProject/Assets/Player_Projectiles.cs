using UnityEngine;
using System.Collections;

public class Player_Projectiles : MonoBehaviour
{
    public GameObject projectile;
    public Transform ProjectileSpawn;

    public float projectileDamage;
    public float projectileSpeed;
    public float projectileLifetime;
    public GameObject Cookie;
    public GameObject Cake;

    // Use this for initialization
    void Start()
    {
        projectile = Cake;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(projectile, ProjectileSpawn.position, ProjectileSpawn.rotation);

        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            projectile = Cookie;
        }

    }
}
