using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingCode : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject setting;

	public void getIntoSetting()
    {
        mainMenu.SetActive(false);
        setting.SetActive(true);
    }

    public void backToMainMenu()
    {
        mainMenu.SetActive(true);
        setting.SetActive(false);
    }
}
