﻿using UnityEngine;
using System.Collections;

public class Projectile_Cookie : MonoBehaviour
{
    public float damage;
    public float speed;
    public float lifetime;
    private float randRotUp;
    private Vector3 hitPos;

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
        if (other.gameObject.tag == "Obstacle"|| other.gameObject.tag == "Cakes_Projectile" || other.gameObject.tag == "Croissants_Projectile" || other.gameObject.tag == "Cookies_Projectile")
        {
            rb.velocity = Vector3.zero;
            hitPos = transform.position;
            randRotUp = 0;
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
        if(randRotUp > 0) transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y + transform.rotation.y * Time.deltaTime, transform.rotation.z);
        if (randRotUp <= 0)
        {
            transform.position = hitPos;
            rb.velocity = Vector3.zero;
        }
    }


}


