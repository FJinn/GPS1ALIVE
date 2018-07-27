using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_Destroy : MonoBehaviour {
    
    public bool countdown = false;
    public float count = 0;
    public int lightOffDuration;
    private bool flickerControl = false;

    private float flickerCount;
    [Header("Flicker happens at % (0 - 1) of its duration")]
    public float firstFlickerTime;
    public float secondFlickerTime;

    [Header("Duration for flickering On and Off (in frames)")]
    public int flickerFrame;

    private void Update()
    {
        if(countdown)
        {
            if (count >= lightOffDuration)
            {
                countdown = false;
                count = 0;
                foreach (Transform child in this.transform)
                {
                    child.GetComponent<Light>().enabled = true;

                    if (child.GetComponent<L_InLight>() != null)
                    {
                        child.GetComponent<L_InLight>().enabled = true;
                        LampFlickerSound();
                    }
                }
            }
            else
            {
                count += Time.deltaTime;
            }

            if(count >= lightOffDuration - (lightOffDuration * firstFlickerTime) && count <= lightOffDuration - (lightOffDuration * secondFlickerTime) && !flickerControl)
            {
                flicker();
            }
            if(count >= lightOffDuration - (lightOffDuration * secondFlickerTime) && flickerControl)
            {
                flicker();
            }
        }
    }

    void flicker()
    {
        if(flickerCount == 0)
        {
            foreach (Transform child in this.transform)
            {
                child.GetComponent<Light>().enabled = true;
                LampFlickerSound();
            }
        }

        if (flickerCount >= flickerFrame)
        {
            foreach (Transform child in this.transform)
            {
                child.GetComponent<Light>().enabled = false;
                LampFlickerSound();
            }
            flickerCount = 0;

            if(!flickerControl)
            {
                flickerControl = true;
            }
            else if(flickerControl)
            {
                flickerControl = false;
            }
        }
        else
        {
            flickerCount++;
        }
    }

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Stone"))
        {
            countdown = true;
            foreach (Transform child in this.transform)
            {
                child.GetComponent<Light>().enabled = false;

                if(child.GetComponent<L_InLight>() != null)
                {
                    child.GetComponent<L_InLight>().enabled = false;
                    LampHitSound();
                }
            }
        }

    }

    public void LampHitSound()
    {
        FindObjectOfType<AudioManager>().Play("LampHit");
    }

    public void LampFlickerSound()
    {
        FindObjectOfType<AudioManager>().Play("LampFlicker");
    }
}
