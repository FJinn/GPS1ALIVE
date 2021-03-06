﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Splash_Screen : MonoBehaviour
{

    public Animator anim;
    public GameObject lvlChanger;
    public int levelToLoad;
    public float sceneDuration;

    // Use this for initialization
    void Start()
    {
        Invoke("setNextScene", sceneDuration);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void setNextScene()
    {
        anim.SetTrigger("FadeOut");
        lvlChanger.GetComponent<LevelChanger>().FadeToLevel(levelToLoad);
    }
}
