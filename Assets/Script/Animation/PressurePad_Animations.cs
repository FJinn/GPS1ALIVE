using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad_Animations : MonoBehaviour {

    private Sprite originalSprite;
    public Sprite changedSprite;

    public GameObject[] myLights;

    private SpriteRenderer mySprite;

    public Color deactivateColor;
    public Color activateColor;

	// Use this for initialization
	void Start () {
        mySprite = GetComponent<SpriteRenderer>();
        originalSprite = mySprite.sprite;
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("Player2") || collision.CompareTag("PushPull") || collision.CompareTag("Enemy"))
        {
            mySprite.sprite = changedSprite;
            for (int i = 0; i < myLights.Length; i++)
            {
                if (myLights[i].GetComponent<Light>() != null)
                {
                    myLights[i].GetComponent<Light>().color = activateColor;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Player2") || collision.CompareTag("PushPull") || collision.CompareTag("Enemy"))
        {
            mySprite.sprite = originalSprite;
            for (int i = 0; i < myLights.Length; i++)
            {
                if (myLights[i].GetComponent<Light>() != null)
                {
                    myLights[i].GetComponent<Light>().color = deactivateColor;
                }
            }
        }
    }
}
