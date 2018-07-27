using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class P_Death : MonoBehaviour {

    public bool isDead;
    public GameObject ScreenFade;
    public int sceneIndex;
    [HideInInspector] public bool killByBox = false;
    [HideInInspector] public bool groundOnBlood = false;
    [HideInInspector] public Vector3 forGroundToCheckBlood;    

    IEnumerator Dead(){
        GameObject.Find("Player2").GetComponent<P_controls>().StopGameControl = true;
        GameObject.Find("Player1").GetComponent<P_controls>().StopGameControl = true;        
        //ScreenFade.GetComponent<LevelChanger>().FadeToLevel(2);
        yield return new WaitForSeconds(3f);
        ScreenFade.GetComponent<LevelChanger>().FadeToLevel(sceneIndex);
       // SceneManager.LoadScene(2);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ScreenFade.GetComponent<LevelChanger>().FadeToLevel(2);
            GameObject.FindGameObjectWithTag("CheckpointManager").GetComponent<Checkpoint>().resetManager();
            Destroy(GameObject.FindGameObjectWithTag("CheckpointManager"));
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ScreenFade.GetComponent<LevelChanger>().FadeToLevel(6);
            GameObject.FindGameObjectWithTag("CheckpointManager").GetComponent<Checkpoint>().resetManager();
            Destroy(GameObject.FindGameObjectWithTag("CheckpointManager"));
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ScreenFade.GetComponent<LevelChanger>().FadeToLevel(7);
            GameObject.FindGameObjectWithTag("CheckpointManager").GetComponent<Checkpoint>().resetManager();
            Destroy(GameObject.FindGameObjectWithTag("CheckpointManager"));
        }
    }

}
