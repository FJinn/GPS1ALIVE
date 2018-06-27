using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Splash_Screen : MonoBehaviour
{

    public Animator anim;
    public GameObject lvlChanger;

    // Use this for initialization
    void Start()
    {
        Invoke("setNextScene", 3f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void setNextScene()
    {
        anim.SetTrigger("FadeOut");
        lvlChanger.GetComponent<LevelChanger>().FadeToLevel(1);
    }
}
