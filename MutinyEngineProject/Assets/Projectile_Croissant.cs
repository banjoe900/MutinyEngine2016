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
    public int splashRadius;
    public GameObject Smear;
    public Color smearColour;

    // Use this for initialization
    void Start()
    {

        gameObject.transform.Rotate(new Vector3(Random.Range(-45,45), 0, 0));
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed);

        

        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Floor")
        {
            var croissant = Instantiate(Smear, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.Euler(Vector3.up)) as GameObject;
            croissant.transform.up = other.contacts[0].normal;
            var rot = Quaternion.RotateTowards(croissant.transform.rotation, Quaternion.LookRotation(rb.velocity.normalized), 360f);
            croissant.transform.Rotate(new Vector3(0, rot.eulerAngles.y, 0));
            croissant.GetComponentInChildren<SpriteRenderer>().color = smearColour;
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerBehavior>().AddSugar(damage);
            Instantiate(croissant_impactParticle, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
        else
        {
            Instantiate(croissant_impactParticle, transform.position, transform.rotation);
            var targets = Physics.OverlapSphere(transform.position, splashRadius);
            foreach (var target in targets)
            {
                if (target != null && !target.isTrigger)
                {
                    PlayerBehavior player = target.GetComponent<PlayerBehavior>();
                    if (player != null)
                    {
                        player.AddSugar(damage);
                    }
                }
            }

                
        }
            

        }
    }
