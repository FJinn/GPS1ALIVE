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
        foreach (Collider2D player in Interactable)
        {
            if(player.CompareTag("Interactable"))
            {
                if (GetComponent<BoxCollider2D>().IsTouching(player.GetComponent<BoxCollider2D>()))
                {
                    if (Input.GetKeyDown(GetComponent<P_controls>().KeyUse) && !GetComponent<P_controls>().StopGameControl)
                    {
                        player.GetComponent<M_Interaction>().UnitTrigger();
                    }
                }
            }
            if (player.CompareTag("levelProceed"))
            {
                if (GetComponent<BoxCollider2D>().IsTouching(player.GetComponent<BoxCollider2D>()))
                {
                    if (Input.GetKeyDown(GetComponent<P_controls>().KeyUse) && !GetComponent<P_controls>().StopGameControl)
                    {
                        ScreenFade.GetComponent<LevelChanger>().FadeToLevel(sceneIndex);
                        Destroy(CheckpointManager);
                    }
                }
            }

        }
            
	}
}
