using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VolumeSettingManager : MonoBehaviour {

    public static VolumeSettingManager instance;

    public bool setting = false;
    
    public Slider BGMSlider;
    public Slider SFXSlider;
    private AudioSource bgm;
    static public Canvas MainMenu;
    public Canvas canvas;
    public AudioClip mainMenuClip;
    public AudioClip inGameClip;
    public AudioClip endGameClip;
    GameObject lastRoomEnd;
    public bool playOnce = false;
    private bool lastRoom = false;
    public static int temp;
    public int currentSceneIndex;

    private void Awake()
    {
        canvas.enabled = false;
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    } 

    void Start () {
        
        bgm = GetComponent<AudioSource>();
        bgm.ignoreListenerVolume = true;
        BGMSlider.onValueChanged.AddListener(delegate { BGMValueChanged(); });        
        SFXSlider.onValueChanged.AddListener(delegate { SFXValueChanged(); });
        setCamera();
    }

    private void Update()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if(temp != currentSceneIndex && (currentSceneIndex != 8 || currentSceneIndex != 9))
        {
            temp = currentSceneIndex;
            playOnce = false;
        }

        if(currentSceneIndex == 1)
        {
            bgm.clip = mainMenuClip;
        }
        else if(currentSceneIndex == 7)
        {
            if(lastRoomEnd == null)
            {
                lastRoomEnd = GameObject.FindGameObjectWithTag("LastRoomBGM");
            }
            else
            {
                if (lastRoomEnd.GetComponent<CutsceneElevator>().triggerOnce)
                {
                    bgm.clip = endGameClip;
                    if(!lastRoom)
                    {
                        lastRoom = true;
                        playOnce = false;
                    }
                }
            }
        }
        else if (currentSceneIndex == 8)
        {
            playOnce = true;
            bgm.clip = endGameClip;
        }
        else if(currentSceneIndex == 9) // main menu credit
        {
            playOnce = true;
            bgm.clip = mainMenuClip;
        }
        else
        {
            bgm.clip = inGameClip;
        }
        
        if (!playOnce)
        {
            playOnce = true;
            bgm.Play();
        }
    }

    public void SettingSwitch()
    {
        if(setting)
        {
            canvas.enabled = false;          
            setting = false;           
            if (MainMenu != null)
            {
                MainMenu.enabled = true;
            }         
        }
        else
        {
            setCamera();
            canvas.enabled = true;
            setting = true;
            if (MainMenu != null)
            {
                MainMenu.enabled = false;
            }
        }
    }

    void BGMValueChanged()
    {
        bgm.volume = BGMSlider.value;
    }

    void SFXValueChanged()
    {
        // set audio volume to userSetVolume
        AudioListener.volume = SFXSlider.value;
    }

    public void setCamera()
    {
        canvas.worldCamera = Camera.main;
    }

    public void defaultSetting()
    {
        BGMSlider.value = 0.5f;
        SFXSlider.value = 0.5f;
        BrightnessManager.instance.slider.value = 0f;
    }
}
