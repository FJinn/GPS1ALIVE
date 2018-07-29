using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;


public class CutsceneEnd : MonoBehaviour {

    private GameObject[] players;
    private bool startWalk;
    private float counter;
    [SerializeField] private PostProcessingProfile levelProfile;
    [SerializeField] private GameObject levelChanger;
    private GameObject CheckpointManager;

    ColorGradingModel.Settings myBasicSettings;

    private void Start()
    {
        CheckpointManager = GameObject.FindGameObjectWithTag("CheckpointManager");
        players = new GameObject[2];
        players[0] = GameObject.FindGameObjectWithTag("Player");
        players[1] = GameObject.FindGameObjectWithTag("Player2");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("Player2"))
        {
            for(int i =0; i< players.Length;i++)
            {

                P_controls tempControl;
                tempControl = players[i].GetComponent<P_controls>();

                tempControl.KeyLeft = KeyCode.None;
                tempControl.KeyRight = KeyCode.None;
                tempControl.KeyDown = KeyCode.None;
                tempControl.KeyUp = KeyCode.None;
                tempControl.KeyUse = KeyCode.None;
                
            tempControl.StopGameControl = true;

                startWalk = true;
            }
        }
    }
    

    private void Update()
    {
        if(startWalk)
        {
            counter += 1 * Time.deltaTime;
            if(counter >= 1f)
            {
                for(int i =0;i < players.Length;i++)
                {
                    Rigidbody2D playersrb2d = players[i].GetComponent<Rigidbody2D>();
                    players[i].GetComponent<P_controls>().StopGameControl = true;
                    playersrb2d.velocity = new Vector2(13f, playersrb2d.velocity.y);
                    players[i].GetComponent<Animator>().SetBool("Idle", false);
                    players[i].GetComponent<Animator>().SetBool("Walking", true);
                    players[i].transform.localScale = new Vector3(1, 1, 1);
                }

                myBasicSettings = levelProfile.colorGrading.settings;

                myBasicSettings.basic.postExposure += 3f * Time.deltaTime;

                if(myBasicSettings.basic.postExposure >= 15f)
                {
                    levelChanger.GetComponent<LevelChanger>().FadeToLevel(8);

                    if(CheckpointManager != null)
                    {
                        CheckpointManager.GetComponent<Checkpoint>().resetManager();
                        Destroy(CheckpointManager);
                    }
                }

                levelProfile.colorGrading.settings = myBasicSettings;
            }
        }
    }
}
