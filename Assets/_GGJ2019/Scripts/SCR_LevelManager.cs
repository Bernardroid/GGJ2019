using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
public class SCR_LevelManager : MonoBehaviour {

    //[Header("Level Manager")]
    SCR_JigsawStageManager jigsawManager;
    public static int currentLevel;
    public GameObject[] rooms;
    public GameObject[] storyText;
    GameObject player;
    int totalKills;
    [Header("Background Related")]
    public Image background;
    public float timeToFade;
    public float timeToLoad;
    [HideInInspector]
    public bool isFading;
    //public Sprite[] maskSprites;

    bool isLoading;
    public GameObject Loading;
    public Slider myLoadingBar;





    //PrivateStuff
    WaitForEndOfFrame endOfFrame = new WaitForEndOfFrame();
	// Use this for initialization
	void Start () {
        //FadeBackground(Color.clear, timeToFade);
        player = GameObject.FindGameObjectWithTag("Player");
        jigsawManager = GetComponent<SCR_JigsawStageManager>();
        for (int i = 0; i < storyText.Length; i++)
        {
            storyText[i].SetActive(false);
        }
        storyText[currentLevel].SetActive(true);
        LoadNextLevel();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void FadeBackground(Color _targetColor, float _timeToChange)
    {
        if(!isFading)
        {
            isFading=true;
            StartCoroutine(IEFadeBackground(background.color, _targetColor, _timeToChange));
        }
    }

    public void LoadNextLevel()
    {
        if(!isLoading)
        {
            isLoading = true;
            StartCoroutine(IELoadNextLevel(currentLevel));
        }
    }

    IEnumerator IEFadeBackground(Color _start, Color _end, float _duration)
    {
        yield return null;
        float timer = 0;

        while (_duration > timer)
        {
            background.color = Color.Lerp(_start, _end, (timer / _duration));
            timer += Time.deltaTime;
            yield return endOfFrame;
        }
        background.color = _end;


        isFading = false;
    }

    IEnumerator IELoadNextLevel(int _nextLevel)
    {
        //Fade To Black
        //background.sprite = maskSprites[_nextLevel];
        FadeBackground(Color.black, timeToLoad);
        yield return endOfFrame;
        yield return new WaitUntil(() => isFading == false);

        //
        for (int i = 0; i < storyText.Length; i++)
        {
            storyText[i].SetActive(false);
        }
        storyText[_nextLevel].SetActive(true);
        yield return new WaitForSeconds(5);
        storyText[_nextLevel].SetActive(false);

        //Level Adjustments, Resets, etc.
        for (int i = 0; i < rooms.Length;i++)
        {
            rooms[i].SetActive(false);
        }
        
        rooms[_nextLevel].gameObject.SetActive(true);
        player.transform.position = jigsawManager.playerSpawn.transform.position;
        player.GetComponent<SCR_PlayerStats>().RegenLife();
        //Fade To Transparent
        FadeBackground(Color.clear,timeToLoad);
        yield return endOfFrame;
        yield return new WaitUntil(() => isFading == false);

        Debug.Log(currentLevel);
        switch(currentLevel)
        {
            case 0:
                {
                    jigsawManager.SpawnEnemies(2);
                }
                break;
            case 1:
                {
                    jigsawManager.SpawnEnemies(3);
                }
                break;
            case 2:
                {
                    jigsawManager.SpawnEnemies(4);
                }
                break;
            case 3:
                {
                    jigsawManager.SpawnEnemies(5);
                }
                break;

        }
        isLoading = false;
    }


    public void RestartLevel()
    {
        //StartCoroutine(LoadScene(SceneManager.GetActiveScene().name));
        Debug.Log("Reiniciar");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitLevel()
    {
        Application.Quit();
    }


    IEnumerator LoadScene(string _scene)
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync(_scene);
        float progress = 0.0f;
        while (!loading.isDone)
        {
            progress = Mathf.Clamp01(loading.progress / 0.9f);
            myLoadingBar.value = progress;
            yield return null;
        }
    }


}
