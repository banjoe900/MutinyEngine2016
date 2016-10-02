using UnityEngine;
using System.Collections;

public class Projectile_Cake : MonoBehaviour
{
    public float damage;
    public float speed;
    public float lifetime;
    public GameObject impactParticle;
    public GameObject[] Smear_List;
    public GameObject Smear;
    public Color smearColour;
    int index;
    

    private Rigidbody rb;
    public GameObject Projectile;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed);
        rb.AddTorque(transform.forward * speed);



        Instantiate(impactParticle, transform.position, transform.rotation);
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision other)
    {
        rb.AddForce(other.contacts[0].normal * speed/5);
        // index = Random.Range(0, Smear_List.Length);
        //Smear = Smear_List[index];
        if (other.gameObject.tag == "Floor")
        {
 

            var cake = Instantiate(Smear,new Vector3(transform.position.x, 0, transform.position.z), Quaternion.Euler(Vector3.up)) as GameObject;
            cake.transform.up = other.contacts[0].normal;
            var rot = Quaternion.RotateTowards(cake.transform.rotation, Quaternion.LookRotation(rb.velocity.normalized), 360f);
            cake.transform.Rotate(new Vector3(0, rot.eulerAngles.y, 0));
            cake.GetComponentInChildren<SpriteRenderer>().color = smearColour;


            Debug.Log("smear");

          
        }
        if (other.gameObject.tag == "Player")
        {
            Instantiate(impactParticle, transform.position, transform.rotation);
            other.gameObject.GetComponent<PlayerBehavior>().AddSugar(damage);
            Destroy(this.gameObject);
        }
    }


}


