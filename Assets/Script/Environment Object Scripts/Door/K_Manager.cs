using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_Manager : MonoBehaviour {
    [SerializeField] public GameObject popupUI;
    [SerializeField] private int keyNumber;
    public GameObject[] p;
    [SerializeField] Sprite keyGoneSprite;
    private SpriteRenderer sr;
    bool keyTaken = false;
    bool cannotTake = false;
    [SerializeField] GameObject[] platforms;

	// Use this for initialization
	void Start () {
        p = new GameObject[2];
        p[0] = GameObject.FindGameObjectWithTag("Player");
        p[1] = GameObject.FindGameObjectWithTag("Player2");
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if(keyTaken)
        {
            popupUI.GetComponent<ButtonPressed>().canSpawn = false;
        }
		for(int i=0; i<p.Length; i++){

            if (!cannotTake && Input.GetKeyDown(p[i].GetComponent<P_controls>().KeyUse) && this.GetComponent<BoxCollider2D>().IsTouching(p[i].GetComponent<BoxCollider2D>()) && p[i].GetComponent<P_keyHold>().keyNum == 0 && !p[i].GetComponent<P_controls>().inTheAir){

                p[i].GetComponent<P_keyHold>().keyNum = keyNumber;
                p[i].GetComponent<P_controls>().openDoor = true;
                sr.sprite = keyGoneSprite;
                cannotTake = true;
                popupUI.GetComponent<ButtonPressed>().Despawn();
                keyTaken = true;
                KeyPickupSound();

                for(int j = 0; j< platforms.Length;j++)
                {
                    platforms[j].GetComponent<M_Trigger>().Trigger();
                }
            }
        }
	}

    public void KeyPickupSound()
    {
        FindObjectOfType<AudioManager>().Play("KeyPickup");
    }

}
