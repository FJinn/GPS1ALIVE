using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_mechanismTrigger : MonoBehaviour {

    public float distance = 1f;
    public LayerMask buttonMask;

    public GameObject ScreenFade;
    public int sceneIndex;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        
        var Interactable = Physics2D.OverlapCircleAll(transform.position, 5f);
        foreach (Collider2D player in Interactable)
        {
            if(player.CompareTag("Interactable"))
            {
                if (GetComponent<BoxCollider2D>().IsTouching(player.GetComponent<BoxCollider2D>()))
                {
                    if (Input.GetKeyDown(GetComponent<P_controls>().KeyUse))
                    {
                        player.GetComponent<M_Interaction>().UnitTrigger();
                    }
                }
            }
            if (player.CompareTag("levelProceed"))
            {
                if (GetComponent<BoxCollider2D>().IsTouching(player.GetComponent<BoxCollider2D>()))
                {
                    if (Input.GetKeyDown(GetComponent<P_controls>().KeyUse))
                    {
                        ScreenFade.GetComponent<LevelChanger>().FadeToLevel(sceneIndex);
                    }
                }
            }

        }
            
	}
}
