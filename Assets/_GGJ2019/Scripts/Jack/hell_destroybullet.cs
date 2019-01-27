using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hell_destroybullet : MonoBehaviour {

    void OnEnable()
    {
        Invoke("Destroy", 10f);
    }
    void Destroy()
    {
        gameObject.SetActive(false);    
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.CompareTag("Player") || other.CompareTag("Floor"))
        {
            this.gameObject.SetActive(false);
        }
    }


}
