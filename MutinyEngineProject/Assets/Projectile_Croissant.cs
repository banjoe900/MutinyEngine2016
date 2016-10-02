using UnityEngine;
using System.Collections;

public class Projectile_Croissant: MonoBehaviour
{
    public float damage;
    public float speed;
    public float lifetime;
    public float returnForce;
    public GameObject croissant_impactParticle;



    private Rigidbody rb;
    public GameObject Croissant;

    // Use this for initialization
    void Start()
    {
        

        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed);

        

        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerBehavior>().AddSugar(damage);
            Instantiate(croissant_impactParticle, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
        else
        {
            Instantiate(croissant_impactParticle, transform.position, transform.rotation);
             }
    }
    // Update is called once per frame
    void Update()
    {
        rb.AddForce(transform.right*( returnForce * Time.deltaTime));
        
    }


}


