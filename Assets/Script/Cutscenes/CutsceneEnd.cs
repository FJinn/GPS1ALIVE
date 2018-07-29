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

    ColorGradingModel.Settings myBasicSettings;

    private void Start()
    {
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
                B_Animations tempAnimation;
                players[i].GetComponent<Animator>().SetBool("Idle", true);
                tempAnimation = players[i].GetComponent<B_Animations>();
                tempControl = players[i].GetComponent<P_controls>();

                tempControl.enabled = false;
                tempAnimation.enabled = false;
                
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

                }

            }
        }
    }
}
