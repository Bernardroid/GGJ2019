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
    [Header("Spawning")]
    public GameObject [] enemyDaddy;
    public int initialSpawn;
    public float spawnTimeOffset;
    bool isSpawning;
    [Header("Winning")]
    SCR_LevelManager levelManager;
    int enemyCount;
    public static int waveKills;

    Queue<GameObject> activeSpawnListPrimary = new Queue<GameObject>();
    Queue<GameObject> activeSpawnListSecondary = new Queue<GameObject>();
    List<GameObject> []enemyPool;
    WaitForSeconds waitSpawnTimeOffset;
    WaitForEndOfFrame endOfFrame;
    private void Awake()
    {
        levelManager = GetComponent<SCR_LevelManager>();
        waitSpawnTimeOffset = new WaitForSeconds(spawnTimeOffset);
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


        for (int j = 0; j < enemySpawns[SCR_LevelManager.currentLevel].transform.childCount; j++)
        {
            if (Random.Range(0, 2) == 0)
            {
                activeSpawnListPrimary.Enqueue(enemySpawns[SCR_LevelManager.currentLevel].transform.GetChild(j).gameObject);
            }
            else
            {
                activeSpawnListSecondary.Enqueue(enemySpawns[SCR_LevelManager.currentLevel].transform.GetChild(j).gameObject);
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
        SetActiveSpawners();
        GameObject temp;
        int spawnCount = activeSpawnListPrimary.Count;
        switch(_level)
        {
            case 0:
                {
                    for (int i = 0; i < spawnCount; i++)
                    {
                        temp = GetPooledEnemy(0);
                        temp.transform.position = activeSpawnListPrimary.Dequeue().transform.position;
                        temp.SetActive(true);
                        enemyCount++;

                    }
                }
                break;
            case 1:
                {
                    for (int i = 0; i < spawnCount; i++)
                    {
                        temp = GetPooledEnemy(1);
                        temp.transform.position = activeSpawnListPrimary.Dequeue().transform.position;
                        temp.SetActive(true);
                        enemyCount++;

                    }
                }
                break;
            case 2:
                {
                    for (int i = 0; i < spawnCount; i++)
                    {
                        temp = GetPooledEnemy(2);
                        temp.transform.position = activeSpawnListPrimary.Dequeue().transform.position;
                        temp.SetActive(true);
                        enemyCount++;

                    }
                }
                break;
            case 3:
                {
                    for (int i = 0; i < spawnCount; i++)
                    {
                        temp = GetPooledEnemy(3);
                        temp.transform.position = activeSpawnListPrimary.Dequeue().transform.position;
                        temp.SetActive(true);
                        enemyCount++;

                    }
                }
                break;
        }

       

        yield return waitSpawnTimeOffset;

        spawnCount = activeSpawnListSecondary.Count;

        switch (_level)
        {
            case 0:
                {
                    for (int i = 0; i < spawnCount; i++)
                    {
                        temp = GetPooledEnemy(0);
                        temp.transform.position = activeSpawnListSecondary.Dequeue().transform.position;
                        temp.SetActive(true);
                        enemyCount++;
                    }
                }
                break;
            case 1:
                {
                    for (int i = 0; i < spawnCount; i++)
                    {
                        temp = GetPooledEnemy(1);
                        temp.transform.position = activeSpawnListSecondary.Dequeue().transform.position;
                        temp.SetActive(true);
                        enemyCount++;
                    }
                }
                break;
            case 2:
                {
                    for (int i = 0; i < spawnCount; i++)
                    {
                        temp = GetPooledEnemy(2);
                        temp.transform.position = activeSpawnListSecondary.Dequeue().transform.position;
                        temp.SetActive(true);
                        enemyCount++;
                    }
                }
                break;
            case 3:
                {
                    for (int i = 0; i < spawnCount; i++)
                    {
                        temp = GetPooledEnemy(3);
                        temp.transform.position = activeSpawnListSecondary.Dequeue().transform.position;
                        temp.SetActive(true);
                        enemyCount++;
                    }
                }
                break;
        }
        

        yield return waitSpawnTimeOffset;

        if(_waves<=0)
        {
            yield return new WaitUntil(() => waveKills >= enemyCount);
            isSpawning = false;
            //if using mementos
            //mementos[SCR_LevelManager.currentLevel].SetActive(true);
            SCR_LevelManager.currentLevel++;

            //the other way
            if (SCR_LevelManager.currentLevel>3)
            {
                /////////////////////////////////////////////////////////////////////
                //Agregar aqui el final??
                /////////////////////////////////////////////////////////////////////
            }
            else
            {
                levelManager.LoadNextLevel();
            }
        }
        else
        {
            StartCoroutine(IESpawnEnemies(_waves-1,SCR_LevelManager.currentLevel));
        }
    }
}
