using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneDropUI : MonoBehaviour {

    private GameObject[] p = new GameObject[2];
    P_throw[] pThrow = new P_throw[2];
    float[] pDropStoneCount = new float[2];
    int[] pDropStoneTime = new int[2];

    Canvas canvas;

    GameObject[] tempUI = new GameObject[2];

    public GameObject indicatorUI;

    public static StoneDropUI instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start () {

        p[0] = GameObject.FindGameObjectWithTag("Player");
        p[1] = GameObject.FindGameObjectWithTag("Player2");
        for(int i=0; i<2; i++)
        {
            pThrow[i] = p[i].GetComponent<P_throw>();
        }

        canvas = GetComponent<Canvas>();
    }
	
	// Update is called once per frame
	void Update () {

        canvas.worldCamera = Camera.main;

        for(int i=0; i<2; i++)
        {
            pDropStoneCount[i] = pThrow[i].dropStoneCount / 3;
            pDropStoneTime[i] = pThrow[i].dropStoneTime;
            if (pThrow[i].throwStance && pThrow[i].canThrow)
            {
                if (tempUI[i] == null)
                {
                    tempUI[i] = (GameObject)Instantiate(indicatorUI, pThrow[i].screenPosition, Quaternion.identity);
                    tempUI[i].transform.SetParent(gameObject.transform, false);
                }
                if (tempUI[i] != null && tempUI[i].GetComponent<Image>().fillAmount == 0)
                {
                    Destroy(tempUI[i]);
                }
                else
                {
                    tempUI[i].GetComponent<Image>().fillAmount =  (pDropStoneTime[i]/ pDropStoneTime[i]) - (pDropStoneCount[i]);
                }
            }
            
        }
	}
}
