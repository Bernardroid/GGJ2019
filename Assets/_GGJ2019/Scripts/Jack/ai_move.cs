﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ai_move : MonoBehaviour {

    public Transform[] waypoint;
    public float speed;
    public int theway;
    public bool patrol = true;
    public Vector3 target;
    public Vector3 enem;
    public Vector3 movedir;
    public Vector3 vel;

    private Animator anima;      
           
  

    private Transform player;
 
    private Animator anime;

    void Start()
    {
        anima = GetComponent<Animator>();
       

    }

    private void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform.position;
        enem = GameObject.FindGameObjectWithTag("Enemy").transform.position;
   
        waypoint[0] = GameObject.FindGameObjectWithTag("Player").transform;

        if (theway<waypoint.Length)
        {
            target = waypoint[theway].position;
            movedir = target - transform.position;
            vel = GetComponent<Rigidbody>().velocity;

            if(movedir.magnitude<1)
            {
                theway++;
                 anima.SetBool("Run", true);

            }else
            {
                vel = movedir.normalized * speed;

            }

        }else
        {
            if(patrol)
            {
                theway = 0;

            }
            else
            {
            vel = Vector3.zero;
            }

          
        }

        GetComponent<Rigidbody>().velocity = vel;
        {
            transform.LookAt(target);
            
          
        }

 

    }
    private void OnCollisionEnter(Collision col)
    {
        if(col.rigidbody)
        {
          Destroy(gameObject);

        }
        
    }




}