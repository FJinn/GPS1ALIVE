using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_Box : MonoBehaviour {

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
	void OnTriggerStay2D (Collider2D other) {
		for(int i=0; i<p.Length; i++){
            if (other.gameObject == p[i] && p[i].GetComponent<P_keyHold>().keyNum == 0 && Input.GetKeyDown(p[i].GetComponent<P_controls>().KeyUse)){
                p[i].GetComponent<P_keyHold>().keyNum = keyNumber;
                sr.sprite = keyGoneSprite;
            }
        }
	}
}
