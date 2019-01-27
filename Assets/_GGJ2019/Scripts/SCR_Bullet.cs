using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Bullet : MonoBehaviour {

    public float speed;
    Rigidbody myRigid;
    bool active;
	// Use this for initialization
	void Start () {
        myRigid = this.GetComponent<Rigidbody>();
        myRigid.velocity = transform.forward * speed;
    }

    private void OnEnable()
    {
        myRigid = this.GetComponent<Rigidbody>();

        Debug.Log("myForward: " + transform.forward);
        myRigid.velocity = transform.forward * speed;
        Debug.Log("AM GOING TO: " + myRigid.velocity);
        Invoke("Deactivate", 5.0f);
    }

    // Update is called once per frame
    void Update ()
    {
        //if (active)
        //{
            myRigid.velocity = transform.forward * speed;
        //}
    }

    void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        this.gameObject.SetActive(false);
        if (collision.transform.CompareTag("Enemy") || collision.transform.CompareTag("Floor"))
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy") || other.CompareTag("Floor"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
