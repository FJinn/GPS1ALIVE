using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditToMenu : MonoBehaviour {

    public GameObject lvlChanger;

	public void toMenu()
    {
        lvlChanger.GetComponent<LevelChanger>().FadeToLevel(1);
    }
}
