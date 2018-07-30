using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour {


    private bool triggerOnce = false;
    [SerializeField] private bool StopAudio = false;
    [SerializeField] private bool PlayAudio = false;
    [SerializeField] string AudioToPlay;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!triggerOnce &&(collision.CompareTag("Player") || collision.CompareTag("Player2")))
        {
            triggerOnce = true;
            if(PlayAudio)
            {
                FindObjectOfType<AudioManager>().Play(AudioToPlay);
            }

            if (StopAudio)
            {
                FindObjectOfType<AudioManager>().Stop(AudioToPlay);
            }
        }
    }


}
