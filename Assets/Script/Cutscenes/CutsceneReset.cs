using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class CutsceneReset : MonoBehaviour {

    [SerializeField] PostProcessingProfile levelProfile;
    
    ColorGradingModel.Settings myBasicSettings;
    private GameObject CheckpointManager;

    // Use this for initialization
    void Start () {
        //Reset profile exposure

        myBasicSettings = levelProfile.colorGrading.settings;

        myBasicSettings.basic.postExposure  = 0f;
        
        levelProfile.colorGrading.settings = myBasicSettings;

        //Delete checkpoint

        CheckpointManager = GameObject.FindGameObjectWithTag("CheckpointManager");

        if (CheckpointManager != null)
        {
            CheckpointManager.GetComponent<Checkpoint>().resetManager();
            Destroy(CheckpointManager);
        }
    }

}
