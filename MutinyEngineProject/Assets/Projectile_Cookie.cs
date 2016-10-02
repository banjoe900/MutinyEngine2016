using UnityEngine;
using System.Collections;

public class Projectile_Cookie : MonoBehaviour
{
    public float damage;
    public float speed;
    public float lifetime;
    private float randRotUp;
    private Vector3 hitPos;
    public GameObject cookie_impactParticle;

    private Rigidbody rb;
    public GameObject Cookie;

    // Use this for initialization
    void Start()
    {
        randRotUp = Random.Range(90f, 720f);

        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed);

        
        
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Obstacle"|| other.gameObject.tag == "Floor")
        {
            Instantiate(cookie_impactParticle, transform.position, transform.rotation);
            Destroy(this.gameObject);
            
        }
        if (other.gameObject.tag == "Player")
        {
            Instantiate(cookie_impactParticle, transform.position, transform.rotation);
            other.gameObject.GetComponent<PlayerBehavior>().AddSugar(damage);
            Destroy(this.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(randRotUp > 0) transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y + transform.rotation.y * Time.deltaTime, transform.rotation.z);
        if (randRotUp <= 0)
        {
            transform.position = hitPos;
            rb.velocity = Vector3.zero;
        }
    }


}


