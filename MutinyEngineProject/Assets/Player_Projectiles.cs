using UnityEngine;
using System.Collections;

public class Player_Projectiles : MonoBehaviour
{
    public GameObject projectile;
    public Transform ProjectileSpawn;

    private int playerNumber;
    private string submit;

    public float projectileDamage;
    public float projectileSpeed;
    public float projectileLifetime;
    public GameObject Cookie;
    public GameObject Cake;

    // Use this for initialization
    void Start()
    {
        playerNumber = GetComponent<PlayerMovement>().playerNumber;
        projectile = Cake;
        submit = string.Format("P{0} Attack", playerNumber);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0) || (Input.GetButtonDown(submit)))
        {
            Instantiate(projectile, ProjectileSpawn.position, ProjectileSpawn.rotation);
            transform.Rotate(Vector3.right * Time.deltaTime);

        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cookie_Powerup")
        {
            projectile = Cookie;
            Destroy(other.gameObject);
            
        }
        if (other.gameObject.tag == "Cake_Powerup")
        {
            projectile = Cake;
            Destroy(other.gameObject);
        }
    }
}
