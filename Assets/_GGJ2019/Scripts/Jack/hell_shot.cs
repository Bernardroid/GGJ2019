using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hell_shot : MonoBehaviour {

    public float speed = 3.0f;
   // public int limit;
    private void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward*speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        this.gameObject.SetActive(false);
    }


}
