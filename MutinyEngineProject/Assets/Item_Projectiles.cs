﻿using UnityEngine;
using System.Collections;

public class Item_Projectiles : MonoBehaviour
{
    public float damage;
    public float speed;
    public float lifetime;
    private float randRotUp;
    private float randRotRight;
    private float randRotForward;
    public bool isCookie;

    private Rigidbody rb;
    public GameObject Cookie;

    // Use this for initialization
    void Start()
    {
        randRotUp = Random.Range(90f, 720f);
        randRotRight = Random.Range(90f, 720f);
        randRotForward = Random.Range(90f, 720f);

        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed);

        if (isCookie == true)
        {
            transform.Rotate(Vector3.up * randRotUp / Time.deltaTime);
        }
        else
        {
            transform.Rotate(Vector3.up * randRotUp / Time.deltaTime);
            transform.Rotate(Vector3.right * randRotRight / Time.deltaTime);
            transform.Rotate(Vector3.forward * randRotForward / Time.deltaTime);

        }
        
        
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Obstacle")
        {

           // Destroy(this.gameObject);
           
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

         }


    }


