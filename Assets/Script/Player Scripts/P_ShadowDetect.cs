using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_ShadowDetect : MonoBehaviour {

    public bool P_isUnderShadow = true;

	// Use this for initialization
	void Start () {
        Invoke("InitialShadow", 0.5f);
	}
	
    void InitialShadow()
    {
        P_isUnderShadow = true;
    }
}
