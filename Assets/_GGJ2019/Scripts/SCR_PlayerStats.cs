using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SCR_PlayerStats : MonoBehaviour {

    public float startingLife;
    float life;
    public Image myLifeBar;
    public GameObject myDeathImage;

	// Use this for initialization
	void Start ()
    {
        life = startingLife;
        myLifeBar.fillAmount = Mathf.Clamp01(life / startingLife);
        myDeathImage.SetActive(false);
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
      
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("bullets"))
        {
            DamagePlayer(other.gameObject.GetComponent<SCR_EnemyDmg>().dmg);
            if (life <= 0)
            {
                //loose
                myLifeBar.fillAmount = 0;
                myDeathImage.SetActive(true);
            }
            else
            {
                myLifeBar.fillAmount = Mathf.Clamp01(life / startingLife);
            }
        }
    }
}
