using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Bullet : MonoBehaviour {

    public float speed;
    Rigidbody myRigid;
	// Use this for initialization
	void Start () {
        myRigid = this.GetComponent<Rigidbody>();
        myRigid.velocity = transform.forward * speed;
    }

    private void OnEnable()
    {
        myRigid = this.GetComponent<Rigidbody>();
        myRigid.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update () {
     
    }
}
