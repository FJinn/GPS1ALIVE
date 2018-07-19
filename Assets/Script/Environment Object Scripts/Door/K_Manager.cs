using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_Manager : MonoBehaviour {

    [SerializeField] private int keyNumber;
    public GameObject[] p;
    [SerializeField] Sprite keyGoneSprite;
    private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        p = new GameObject[2];
        p[0] = GameObject.FindGameObjectWithTag("Player");
        p[1] = GameObject.FindGameObjectWithTag("Player2");
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		for(int i=0; i<p.Length; i++){
            if (this.GetComponent<BoxCollider2D>().IsTouching(p[i].GetComponent<BoxCollider2D>()) && p[i].GetComponent<P_keyHold>().keyNum == 0){
                p[i].GetComponent<P_keyHold>().keyNum = keyNumber;
                sr.sprite = keyGoneSprite;
            }
        }
	}
}
