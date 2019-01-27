using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_PlayerStats : MonoBehaviour {

    public float life;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void DamagePlayer(float _dmg)
    {
        life -= _dmg;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Enemy"))
        {
            DamagePlayer(collision.gameObject.GetComponent<SCR_EnemyDmg>().dmg);
        }
    }
}
