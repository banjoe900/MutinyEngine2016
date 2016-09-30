﻿using UnityEngine;
using System.Collections;

public class Item_Projectiles : MonoBehaviour
{
    public float damage;
    public float speed;
    public float lifetime;
    public float randRotUp;
    public float randRotRight;
    public float randRotForward;
    public bool isCookie;

    private Rigidbody rb;
    public GameObject Cookie;

    // Use this for initialization
    void Start()
    {
        if (isCookie == true)
        {
            randRotUp = Random.Range(90f, 720f);
        }
        else
        {
            
            randRotRight = Random.Range(90f, 720f);
            randRotForward = Random.Range(90f, 720f);
        }
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter(Collider other)
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
            transform.Rotate(Vector3.up * Time.deltaTime * randRotUp);
            transform.Rotate(Vector3.right * Time.deltaTime * randRotRight);
            transform.Rotate(Vector3.forward * Time.deltaTime * randRotForward);
         }


    }


