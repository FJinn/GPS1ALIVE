using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Paused : MonoBehaviour
{

    public static bool GameIsPauseed = false;
    public GameObject pauseMenuUi;
    public GameObject levelChanger;
    public GameObject setting;

    private GameObject CheckpointManager;

    private void Start()
    {
        CheckpointManager = GameObject.FindGameObjectWithTag("CheckpointManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPauseed)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        GameIsPauseed = false;
    }

    void Pause()
    {
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        GameIsPauseed = true;
    }

    // setting
    public void Settings()
    {
        pauseMenuUi.SetActive(false);
        setting.SetActive(true);
    }

    //setting back to pause menu
    public void backToPauseMenu()
    {
        pauseMenuUi.SetActive(true);
        setting.SetActive(false);
    }

    // skip tutorial
    public void Skip()
    {
        Time.timeScale = 1f;
        levelChanger.GetComponent<LevelChanger>().FadeToLevel(2);
        Destroy(CheckpointManager);
        Debug.Log("Skipping Tutorial");
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        levelChanger.GetComponent<LevelChanger>().FadeToLevel(1);
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
