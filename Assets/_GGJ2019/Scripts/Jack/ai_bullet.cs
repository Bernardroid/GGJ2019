using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ai_bullet : MonoBehaviour {

    public GameObject shot;
    public Transform bspawn;
    public float firerate;
    private float next;

    public int poolammo;
    List<GameObject> shots;
    WaitForSeconds myWait;
    void Start()
    {
        /*shots = new List<GameObject>();

        for (int i= 0; i<poolammo;i++)
        {
            GameObject obj = (GameObject)Instantiate(shot);
            obj.SetActive(false);
            shots.Add(obj);

        }
        */
        myWait = new WaitForSeconds(firerate);
        StartCoroutine(Fire());
        //InvokeRepeating("fire", firerate, firerate);   
    }
   /* void Update () {
	
        if(Time.time>next)
        {
            next = Time.time + firerate;
            Instantiate(shot, bspawn.position, bspawn.rotation);

        }
        

	}*/

    IEnumerator Fire()
    {
        yield return myWait;

        GameObject obj = hell_poolbullet.current.Getpoolshot();
        //Instantiate(shot, transform.position, Quaternion.identity);
        if (obj != null)
        {
            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;
            obj.SetActive(true);
        }
        StartCoroutine(Fire());
    }

}


