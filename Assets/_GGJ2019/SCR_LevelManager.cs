﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SCR_LevelManager : MonoBehaviour {
    [Header("Level Manager")]
    public SCR_LevelManager[] levels;
    public int currentLevel;


    [Header("Background Related")]
    public Image background;
    public float timeToFade;
    public float timeToLoad;
    bool isFading;
    bool isLoading;


    //PrivateStuff
    WaitForEndOfFrame endOfFrame = new WaitForEndOfFrame();
	// Use this for initialization
	void Start () {
        //FadeBackground(Color.clear, timeToFade);
        LoadNextLevel();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void FadeBackground(Color _targetColor, float _timeToChange)
    {
        if(!isFading)
        {
            isFading=true;
            StartCoroutine(IEFadeBackground(background.color, _targetColor, _timeToChange));
        }
    }

    void LoadNextLevel()
    {
        if(!isLoading)
        {
            isLoading = true;
            StartCoroutine(IELoadNextLevel(++currentLevel));
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
        FadeBackground(Color.black,timeToLoad);
        yield return endOfFrame;
        yield return new WaitUntil(() => isFading == false);

        //Level Adjustments, Resets, etc.
        levels[_nextLevel].gameObject.SetActive(true);

        //Fade To Transparent
        FadeBackground(Color.clear,timeToLoad);
        yield return endOfFrame;
        yield return new WaitUntil(() => isFading == false);

        isLoading = false;
    }

    
}
