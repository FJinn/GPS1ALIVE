using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingButton : MonoBehaviour {

    VolumeSettingManager vsm;

    private void Start()
    {
        vsm = VolumeSettingManager.instance;
    }

    public void OnClick()
    {
        vsm.SettingSwitch();
    }
}
