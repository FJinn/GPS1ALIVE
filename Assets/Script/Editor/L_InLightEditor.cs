using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor (typeof(L_InLight))]
public class L_InLightEditor : Editor {
    
	void OnSceneGUI () {
        L_InLight targetLight = (L_InLight)target;
        Handles.color = Color.yellow;
        Handles.DrawWireArc(targetLight.transform.position, Vector3.forward, Vector3.right, 360, targetLight.LightDetectionRadius);

	}
	
}
