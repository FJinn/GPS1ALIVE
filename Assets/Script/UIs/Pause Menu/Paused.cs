using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Paused : MonoBehaviour
{

    public static bool GameIsPauseed = false;
    public GameObject pauseMenuUi;
    public GameObject levelChanger;
    VolumeSettingManager vsm;

    private GameObject CheckpointManager;

    private void Start()
    {
        CheckpointManager = GameObject.FindGameObjectWithTag("CheckpointManager");
        vsm = VolumeSettingManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!vsm.setting)
            {
                if (GameIsPauseed)
                {
                    Resume();
                }
                else
                {
                    Pause();
                    vsm.setCamera();
                }
            }            
        }
    }

    public void Resume()
    {
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        GameIsPauseed = false;
        vsm.canvas.enabled = false;
        vsm.setting = false;
    }

    void Pause()
    {
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        GameIsPauseed = true;
    }

    // skip tutorial
    public void Skip()
    {
        Time.timeScale = 1f;
        levelChanger.GetComponent<LevelChanger>().FadeToLevel(2);
        if(CheckpointManager != null)
        {
            CheckpointManager.GetComponent<Checkpoint>().resetManager();
            Destroy(CheckpointManager);
        }
        Debug.Log("Skipping Tutorial");
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        levelChanger.GetComponent<LevelChanger>().FadeToLevel(1);
        if (CheckpointManager != null)
        {
            CheckpointManager.GetComponent<Checkpoint>().resetManager();
            Destroy(CheckpointManager);
        }
        Debug.Log("Loading Menu");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }
}
