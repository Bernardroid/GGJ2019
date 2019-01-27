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
    public GameObject [] enemyDaddy;
    public int initialSpawn;
    public float spawnTimeOffset;
    public float maxEnemies;
    private bool activeSpawner; //false=primary, true=secondary
    bool isSpawning;
    int currentEnemies;
    int waveKills;

    Queue<GameObject> activeSpawnListPrimary = new Queue<GameObject>();
    Queue<GameObject> activeSpawnListSecondary = new Queue<GameObject>();
    List<GameObject> []enemyPool;
    WaitForSeconds waitForSeconds;
    WaitForEndOfFrame endOfFrame;
    private void Awake()
    {
        waitForSeconds = new WaitForSeconds(spawnTimeOffset);
        enemyPool = new List<GameObject>[enemyTypes.Length];
        for(int i=0; i<enemyTypes.Length;i++)
        {
            enemyPool[i] = new List<GameObject>();
        }
    }
    // Use this for initialization
    void Start() {
        for(int i=0;i< enemyTypes.Length;i++)
        {
            InstantiateEnemyPool(i);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    #region POOLING
    void InstantiateEnemyPool(int _enemyType)
    {
        GameObject temp;
        for (int i=0; i<initialSpawn; i++)
        {
            temp = Instantiate(enemyTypes[_enemyType], enemyDaddy[_enemyType].transform);
            enemyPool[_enemyType].Add(temp);
            temp.SetActive(false);
        }
    }
    GameObject GetPooledEnemy(int _enemyType)
    {

        for (int i = 0; i < enemyPool[_enemyType].Count; i++)
        {
            if (!enemyPool[_enemyType][i].activeSelf)
            {
                enemyPool[_enemyType][i].SetActive(true);
                return enemyPool[_enemyType][i];
            }
        }

        GameObject temp = Instantiate(enemyTypes[_enemyType], enemyDaddy[_enemyType].transform);
        enemyPool[_enemyType].Add(temp);
        return temp;

    }
    #endregion

    #region SPAWNING
    void SetActiveSpawners()
    {
        activeSpawnListPrimary.Clear();
        activeSpawnListSecondary.Clear();

        for (int i = 0; i < enemySpawns.Length; i++)
        {
            for(int j=0; j< enemySpawns[i].transform.childCount;j++)
            {
                if (Random.Range(0, 2) == 0)
                {
                    activeSpawnListPrimary.Enqueue(enemySpawns[i].transform.GetChild(j).gameObject);
                }
                else
                {
                    activeSpawnListSecondary.Enqueue(enemySpawns[i].transform.GetChild(j).gameObject);
                }
            }
            
        }
    }
    public void SpawnEnemies(int _waves)
    {
        if (!isSpawning)
        {
            isSpawning = true;
            StartCoroutine(IESpawnEnemies(_waves, SCR_LevelManager.currentLevel));
        }
    }
    #endregion


    IEnumerator IESpawnEnemies(int _waves, int _level)
    {
        int spawnCount = activeSpawnListPrimary.Count;

        for (int i = 0; i < spawnCount; i++)
        {
            GetPooledEnemy(0).transform.position = activeSpawnListPrimary.Dequeue().transform.position;
        }

        yield return waitForSeconds;

        spawnCount = activeSpawnListSecondary.Count;
        for (int i = 0; i < spawnCount; i++)
        {
            GetPooledEnemy(0).transform.position = activeSpawnListPrimary.Dequeue().transform.position;
        }

        yield return waitForSeconds;

        if(_waves<=0)
        {
            isSpawning = true;
        }
        else
        {
            StartCoroutine(IESpawnEnemies(_waves-1,SCR_LevelManager.currentLevel));
        }
    }
}
