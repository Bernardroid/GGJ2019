using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SCR_PlayerStats : MonoBehaviour {

    public float startingLife;
    float life;
    public Image myLifeBar;
    public GameObject myDeathImage;
    public Material[] myMats;
    public SkinnedMeshRenderer myRenderer;
    bool hit = false;
    bool pair = true;
    WaitForSeconds myWait = new WaitForSeconds(0.1f);
    int frames = 30;

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
    public void RegenLife()
    {
        life = startingLife;
        myLifeBar.fillAmount = Mathf.Clamp01(life / startingLife);

    }
    private void OnCollisionEnter(Collision collision)
    {
      
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("bullets") && !hit)
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
                StartCoroutine(Interrupting());
            }
        }
    }

    IEnumerator Interrupting()
    {
        hit = true;
        for (int i = 0; i < frames; i++)
        {
            if (pair)
            {
                myRenderer.material = myMats[1];
            }
            else
            {
                myRenderer.material = myMats[0];

            }
            pair = !pair;
            yield return myWait;
        }
        myRenderer.material = myMats[0];
        hit = false;
    }
}
