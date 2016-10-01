using UnityEngine;
using System.Collections;

public class Projectile_Cake : MonoBehaviour
{
    public float damage;
    public float speed;
    public float lifetime;
    private float randRotUp;
    private float randRotRight;
    private float randRotForward;
    public GameObject impactParticle;
    public GameObject[] Smear_List;
    public GameObject Smear;
    int index;
    

    private Rigidbody rb;
    public GameObject Projectile;

    // Use this for initialization
    void Start()
    {
        randRotUp = Random.Range(90f, 720f);
        randRotRight = Random.Range(90f, 720f);
        randRotForward = Random.Range(90f, 720f);

        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed);



            Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision other)
    {
       // index = Random.Range(0, Smear_List.Length);
       //Smear = Smear_List[index];
        if (other.gameObject.tag == "Obstacle")
        {
 

            var cake = Instantiate(Smear,new Vector3(transform.position.x, 0, transform.position.z), Quaternion.Euler(Vector3.up)) as GameObject;
            cake.transform.up = other.contacts[0].normal;
            var rot = Quaternion.RotateTowards(cake.transform.rotation, Quaternion.LookRotation(rb.velocity.normalized), 360f);
            cake.transform.Rotate(new Vector3(0, rot.eulerAngles.y, 0));


            Debug.Log("smear");

          
        }
        if (other.gameObject.tag == "Player")
        {
            Instantiate(impactParticle, transform.position, transform.rotation);
            other.gameObject.GetComponent<PlayerBehavior>().AddSugar(damage);
            Destroy(this.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * randRotUp * Time.deltaTime);
        transform.Rotate(Vector3.right * randRotRight * Time.deltaTime);
        transform.Rotate(Vector3.forward * randRotForward * Time.deltaTime);
    }


}


