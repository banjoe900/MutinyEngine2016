﻿using UnityEngine;
using System.Collections;

public class Projectile_Croissant: MonoBehaviour
{
    public float damage;
    public float speed;
    public float lifetime;
    private float randRotUp;

    private Rigidbody rb;
    public GameObject Croissant;

    // Use this for initialization
    void Start()
    {
        randRotUp = Random.Range(360f, 720f);

        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed);

        

        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Obstacle")
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
        transform.Rotate(Vector3.up * randRotUp * Time.deltaTime);
        rb.AddForce(transform.forward*( -300 * Time.deltaTime));
    }


}


