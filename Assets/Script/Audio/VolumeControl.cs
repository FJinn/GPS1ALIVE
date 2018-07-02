using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeControl : MonoBehaviour {

    public GameObject player;
    public AudioSource bgm;
    public float maxDistance;
    float dist;
    Vector2 posA;
    Vector2 posB;

    void Update()
    {
        checkDistance();
        setVolume();
    }

    void checkPosition()
    {
        posA = player.transform.position;
        posB = transform.position;
    }

    void checkDistance()
    {
        dist = Vector3.Distance(player.GetComponent<Transform>().position, transform.position);
    }

    public float AngleDir(Vector2 A, Vector2 B)
    {
        return -A.x * B.y + A.y * B.x;
    }

    void setVolume()
    {
        float rangeBetween;
        float sfxVolume;
        float bgmVolume;
        if (dist <= maxDistance)
        {
            rangeBetween = dist / maxDistance;
            bgmVolume = rangeBetween;
            bgm.volume = bgmVolume;
            sfxVolume = 1 - bgmVolume;
            GetComponent<AudioSource>().volume = sfxVolume;
        }
    }

    void stereoPan()
    {
        if (AngleDir(posA, posB) < 0)
        {
            if (dist > 10)
            {
                GetComponent<AudioSource>().panStereo = -1f;
            }
            else if (dist > 9 && dist < 10)
            {
                GetComponent<AudioSource>().panStereo = -0.9f;
            }
            else if (dist > 8 && dist < 9)
            {
                GetComponent<AudioSource>().panStereo = -0.8f;
            }
            else if (dist > 7 && dist < 8)
            {
                GetComponent<AudioSource>().panStereo = -0.7f;
            }
            else if (dist > 6 && dist < 7)
            {
                GetComponent<AudioSource>().panStereo = -0.6f;
            }
            else if (dist > 5 && dist < 6)
            {
                GetComponent<AudioSource>().panStereo = -0.5f;
            }
            else if (dist > 4 && dist < 5)
            {
                GetComponent<AudioSource>().panStereo = -0.4f;
            }
            else if (dist > 3 && dist < 4)
            {
                GetComponent<AudioSource>().panStereo = -0.3f;
            }
            else if (dist > 2 && dist < 3)
            {
                GetComponent<AudioSource>().panStereo = -0.2f;
            }
            else if (dist > 1 && dist < 2)
            {
                GetComponent<AudioSource>().panStereo = -0.1f;
            }
            else if (dist > 0 && dist < 1)
            {
                GetComponent<AudioSource>().panStereo = 0f;
            }
        }
        else if (AngleDir(posA, posB) > 0)
        {
            if (dist > 10)
            {
                GetComponent<AudioSource>().panStereo = 1f;
            }
            else if (dist > 9 && dist < 10)
            {
                GetComponent<AudioSource>().panStereo = 0.9f;
            }
            else if (dist > 8 && dist < 9)
            {
                GetComponent<AudioSource>().panStereo = 0.8f;
            }
            else if (dist > 7 && dist < 8)
            {
                GetComponent<AudioSource>().panStereo = 0.7f;
            }
            else if (dist > 6 && dist < 7)
            {
                GetComponent<AudioSource>().panStereo = 0.6f;
            }
            else if (dist > 5 && dist < 6)
            {
                GetComponent<AudioSource>().panStereo = 0.5f;
            }
            else if (dist > 4 && dist < 5)
            {
                GetComponent<AudioSource>().panStereo = 0.4f;
            }
            else if (dist > 3 && dist < 4)
            {
                GetComponent<AudioSource>().panStereo = 0.3f;
            }
            else if (dist > 2 && dist < 3)
            {
                GetComponent<AudioSource>().panStereo = 0.2f;
            }
            else if (dist > 1 && dist < 2)
            {
                GetComponent<AudioSource>().panStereo = 0.1f;
            }
            else if (dist > 0 && dist < 1)
            {
                GetComponent<AudioSource>().panStereo = 0f;
            }
            else
            {
                GetComponent<AudioSource>().panStereo = 0;
            }
        }
    }
}
