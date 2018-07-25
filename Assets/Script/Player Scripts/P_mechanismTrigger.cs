using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_mechanismTrigger : MonoBehaviour {

    public float distance = 1f;
    public LayerMask buttonMask;

    public GameObject ScreenFade;
    public bool triggerLever = false;
    public bool leverDown = false;
    public bool leverUp = false;

    private GameObject CheckpointManager;
    private BoxCollider2D myBoxCollider;

    private void Start()
    {
        myBoxCollider = GetComponent<BoxCollider2D>();
        CheckpointManager = GameObject.FindGameObjectWithTag("CheckpointManager");
    }

    // Update is called once per frame
    void Update () {

        
        Collider2D[] Interactable = Physics2D.OverlapCircleAll(transform.position, 5f);
        foreach (Collider2D mechanism in Interactable)
        {
            if(mechanism.CompareTag("Interactable"))
            {
                if (myBoxCollider.IsTouching(mechanism.GetComponent<BoxCollider2D>()))
                {
                    if (Input.GetKeyDown(GetComponent<P_controls>().KeyUse) && !GetComponent<P_controls>().StopGameControl && GetComponent<P_controls>().Grounded())
                    {
                        mechanism.GetComponent<M_Interaction>().UnitTrigger();
                        triggerLever = true;
                        FindObjectOfType<AudioManager>().Play("LeverPulling");
                    }                 
                }
            }
            if (mechanism.CompareTag("levelProceed"))
            {
                if (myBoxCollider.IsTouching(mechanism.GetComponent<BoxCollider2D>()))
                {
                    if (Input.GetKeyDown(GetComponent<P_controls>().KeyUse) && !GetComponent<P_controls>().StopGameControl && mechanism.GetComponent<D_levelProceed>().player1Enter && mechanism.GetComponent<D_levelProceed>().player2Enter)
                    {
                        ScreenFade.GetComponent<LevelChanger>().FadeToLevel(mechanism.GetComponent<D_levelProceed>().levelIndex);
                        CheckpointManager.GetComponent<Checkpoint>().resetManager();
                        Destroy(CheckpointManager);
                    }
                }
            }

        }
            
	}
}
