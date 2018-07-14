using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCode : MonoBehaviour {

    public GameObject mainMenu;
   // public GameObject setting;
    public GameObject credits;
    public GameObject credits2;
    public GameObject credits3;    
/*
	public void getIntoSetting()
    {
        mainMenu.SetActive(false);
        setting.SetActive(true);
    }
    */
    public void backToMainMenu()
    {
        mainMenu.SetActive(true);
  //      setting.SetActive(false);
        credits.SetActive(false);
        credits2.SetActive(false);
        credits3.SetActive(false);
    }

    public void getIntoCredits()
    {
        mainMenu.SetActive(false);
        credits.SetActive(true);
    }

    public void creditsPage1()
    {
        credits.SetActive(true);
        credits2.SetActive(false);
        credits3.SetActive(false);
    }

    public void creditsPage2()
    {
        credits.SetActive(false);
        credits2.SetActive(true);
        credits3.SetActive(false);
    }

    public void creditsPage3()
    {
        credits.SetActive(false);
        credits2.SetActive(false);
        credits3.SetActive(true);
    }
}
