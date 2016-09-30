using UnityEngine;
using System.Collections;

public class Item_Projectiles : MonoBehaviour
{
    public float damage;
    public float speed;
    public float lifetime;

    private Rigidbody rb;
    public GameObject Cookie;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {

            Destroy(this.gameObject);
            //   Projectile.projectile = Cookie;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
