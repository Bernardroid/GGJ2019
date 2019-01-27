using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [System.Serializable]
    public class bounds
    {
        public float xmin, xmax, zmin, zmax;
    }


public class hell_movement : MonoBehaviour {

    public float speed;
    public bounds bound;

    public GameObject bullet;
    public Transform bulletspawn;
    public float firerate;

    private float nextFire;

    private void Update()
    {
        if(Time.time>nextFire)
        {
            nextFire = Time.time + firerate;
            GameObject clone = Instantiate(bullet, transform.position, transform.rotation);

        }


    }
    private void FixedUpdate()
    {
        float movehorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");


        var movement = new Vector3(movehorizontal, 0.0f, moveVertical);
        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = movement * speed;
       // rigidbody.position = new Vector3;
    }




}
