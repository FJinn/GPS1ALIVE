using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_mechanismTrigger : MonoBehaviour {

    public float distance = 1f;
    public LayerMask buttonMask;

    public GameObject ScreenFade;
    public int sceneIndex;

    private GameObject CheckpointManager;

    private void Start()
    {
        CheckpointManager = GameObject.FindGameObjectWithTag("CheckpointManager");
    }

    // Update is called once per frame
    void Update () {

        
        Collider2D[] Interactable = Physics2D.OverlapCircleAll(transform.position, 5f);
        foreach (Collider2D mechanism in Interactable)
        {
            if(mechanism.CompareTag("Interactable"))
            {
                if (GetComponent<BoxCollider2D>().IsTouching(mechanism.GetComponent<BoxCollider2D>()))
                {
                    if (Input.GetKeyDown(GetComponent<P_controls>().KeyUse) && !GetComponent<P_controls>().StopGameControl)
                    {
                        mechanism.GetComponent<M_Interaction>().UnitTrigger();
                    }
                }
            }
            if (mechanism.CompareTag("levelProceed"))
            {
                if (GetComponent<BoxCollider2D>().IsTouching(mechanism.GetComponent<BoxCollider2D>()))
                {
                    if (Input.GetKeyDown(GetComponent<P_controls>().KeyUse) && !GetComponent<P_controls>().StopGameControl)
                    {
                        ScreenFade.GetComponent<LevelChanger>().FadeToLevel(sceneIndex);
                        CheckpointManager.GetComponent<Checkpoint>().spawnPoint = new Vector3(-0.3f, 3, 0);
                    }
                }
            }

        }
            
	}
}
