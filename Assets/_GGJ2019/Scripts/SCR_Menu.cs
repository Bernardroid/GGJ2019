using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCR_Menu : MonoBehaviour {

    public Image Background;
    public Sprite[] myBgImages;
    WaitForSeconds myWait = new WaitForSeconds(0.08f);
    bool pair = true;
    float timerInt = 0;
    float myTime = 1.0f;
	// Use this for initialization
	void Start () {
        StartCoroutine(Interrupting());
    }
	
	// Update is called once per frame
	void Update ()
    {
  //      timerInt += Time.deltaTime;
		//if(timerInt >= myTime)
  //      {
            
  //      }
	}

    IEnumerator Interrupting()
    {
        int ranFrames = Random.Range(1, 15);
        myTime = float.MaxValue;
        for (int i = 0; i < ranFrames; i++)
        {
            if(pair)
            {
                Background.sprite = myBgImages[1];
            }
            else
            {
                Background.sprite = myBgImages[0];

            }
            pair = !pair;
            yield return myWait;
        }

        myTime = Random.Range(2.0f, 5.0f);
        Debug.Log(myTime);
        yield return new WaitForSeconds(myTime);
        StartCoroutine(Interrupting());
        
    }
}
