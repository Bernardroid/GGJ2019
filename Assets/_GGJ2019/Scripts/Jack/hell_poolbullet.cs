using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hell_poolbullet : MonoBehaviour {


    public static  hell_poolbullet current;
    public GameObject poolshot;
    public int poolammo;
    public bool willGrow=true;
    public List<GameObject> pooledshots;

    void Awake()
    {
     current= this;     
    }

    void Start()
    {
        pooledshots = new List<GameObject>();
        for(int i=0;i<poolammo;i++)
        {
            GameObject obj = (GameObject)Instantiate(poolshot);
            obj.SetActive(false);
            pooledshots.Add(obj);
        }
    }

    public GameObject Getpoolshot()
    {
        for(int i =0; i<pooledshots.Count;i++)
        {
            if(!pooledshots[i].activeInHierarchy)
            {
                return pooledshots[i];
            }

        }
        if(willGrow)
        {
            GameObject obj = (GameObject)Instantiate(poolshot);
            pooledshots.Add(obj);
            return obj;
        }
        return null;

    }


}
