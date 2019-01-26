using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_JigsawStageManager : MonoBehaviour {

    [Header("Enemies")]
    public GameObject[] enemySpawns;
    public GameObject[] enemyTypes;
    [Header("Player")]
    public GameObject playerSpawn;
    public GameObject[] mementos;
    public GameObject mementoSpawn;
    [Header("Gameplay")]
    public GameObject enemyDaddy;
    public float spawnTimeOffset;
    public float totalEnemies;
    private bool activeSpawner; //true=primary, false=secondary
    bool spawning;
    public int initialSpawn;
    List<GameObject> activeSpawnListPrimary = new List<GameObject>();
    List<GameObject> activeSpawnListSecondary = new List<GameObject>();
    List<GameObject> enemyPool = new List<GameObject>();
    WaitForSeconds waitForSeconds;

    // Use this for initialization
    void Start() {
        waitForSeconds = new WaitForSeconds(spawnTimeOffset);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    #region POOLING
    void InstantiateEnemyPool()
    {
        GameObject temp;
        for (int i=0; i<initialSpawn; i++)
        {
            temp = Instantiate(enemyTypes[0]);
            enemyPool.Add(temp);
            temp.SetActive(false);
        }
    }
    void EnemyPooling()
    {

    }
    #endregion

    #region SPAWNING
    void SetActiveSpawners()
    {
        activeSpawnListPrimary.Clear();
        activeSpawnListSecondary.Clear();

        for (int i = 0; i < enemySpawns.Length; i++)
        {
            if (Random.Range(0, 2) == 0)
            {
                activeSpawnListPrimary.Add(enemySpawns[i]);
            }
            else
            {
                activeSpawnListSecondary.Add(enemySpawns[i]);
            }
        }
    }
    void SpawnEnemies()
    {
        if(!spawning)
        {
            spawning = true;
            StartCoroutine(IESpawnEnemies());
        }
    }
    #endregion


    IEnumerator IESpawnEnemies()
    {

        yield return waitForSeconds;

    }
}
