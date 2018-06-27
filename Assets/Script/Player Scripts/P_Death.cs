using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class P_Death : MonoBehaviour {

    public bool isDead;
    public Animator ScreenFade;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    IEnumerator Dead()
    {
        GameObject.FindGameObjectWithTag("Player2").GetComponent<P_controls>().StopGameControl = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<P_controls>().StopGameControl = true;
        ScreenFade.SetTrigger("FadeOut");
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(2);
    }
    
}
