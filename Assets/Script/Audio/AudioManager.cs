using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour {

    public static bool oneAudio;

    public Sound[] sounds;
    
	// Use this for initialization
	void Awake () {

        if(oneAudio == false)
        {
            oneAudio = true;
        }else
        {
            Destroy(this.gameObject);
        }


		foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

   //     Play("Environmental");
	}

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
 //       Play("Environmental");
    }
    

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
    
}
