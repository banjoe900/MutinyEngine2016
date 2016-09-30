using UnityEngine;
using System.Collections;

public class Projectile_Croissant: MonoBehaviour
{
    public float damage;
    public float speed;
    public float lifetime;
    private float randRotUp;

    private Rigidbody rb;
    public GameObject Cookie;

    // Use this for initialization
    void Start()
    {
        randRotUp = Random.Range(90f, 720f);

        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed);

        transform.Rotate(Vector3.up * randRotUp * Time.deltaTime);

        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Obstacle" || other.gameObject.tag == "Projectile")
        {
            Destroy(this.gameObject);

        }
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerBehavior>().AddSugar(damage);
            Destroy(this.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        rb.AddForce(transform.forward*( -300 * Time.deltaTime));
    }


}


