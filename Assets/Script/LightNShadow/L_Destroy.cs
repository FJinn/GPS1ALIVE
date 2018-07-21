using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_Destroy : MonoBehaviour {
    
    public bool countdown = false;
    public float count = 0;
    public int lightOffDuration;

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
                    }
                }
            }
            else
            {
                count += Time.deltaTime;
            }
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
                }
            }
        }

    }
}
