using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class P_Death : MonoBehaviour {

    public bool isDead;
    public GameObject ScreenFade;
    public int sceneIndex;

    IEnumerator Dead(){
        GameObject.Find("Player2").GetComponent<P_controls>().StopGameControl = true;
        GameObject.Find("Player1").GetComponent<P_controls>().StopGameControl = true;
        //ScreenFade.GetComponent<LevelChanger>().FadeToLevel(2);
        yield return new WaitForSeconds(5f);
        ScreenFade.GetComponent<LevelChanger>().FadeToLevel(sceneIndex);
       // SceneManager.LoadScene(2);
    }
    
}
