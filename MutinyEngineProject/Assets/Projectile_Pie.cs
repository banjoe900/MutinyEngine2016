using UnityEngine;
using System.Collections;

public class Projectile_Pie : MonoBehaviour
{
    public float damage;
    public float splashRadius;
    public float torque;
    public float lifetime;
    public GameObject impactParticle;
    public GameObject Smear;
    public Color smearColour;
    int index;
    

    private Rigidbody rb;
    public GameObject Projectile;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddTorque(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)) * torque);
        
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Floor")
        {
            var decal = Instantiate(Smear,new Vector3(transform.position.x, 0, transform.position.z), Quaternion.Euler(Vector3.up)) as GameObject;
            decal.transform.up = other.contacts[0].normal;
            var rot = Quaternion.RotateTowards(decal.transform.rotation, Quaternion.LookRotation(rb.velocity.normalized), 360f);
            decal.transform.Rotate(new Vector3(0, rot.eulerAngles.y, 0));
            decal.GetComponentInChildren<SpriteRenderer>().color = smearColour;
            decal.transform.localScale *= 1.4f;
            Splash();
            Destroy(this.gameObject);

        }
    }

    void Splash()
    {
        var targets = Physics.OverlapSphere(transform.position, splashRadius);
        foreach (var target in targets)
        {
            if (target != null && !target.isTrigger)
            {
                PlayerBehavior player = target.GetComponent<PlayerBehavior>();
                if (player != null)
                {
                    player.AddSugar(damage * Vector3.Distance(target.transform.position, transform.position) / splashRadius);
                }
            }
        }
    }

    void OnDestroy()
    {
        Instantiate(impactParticle, transform.position, transform.rotation);
    }
}


