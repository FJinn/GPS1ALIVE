using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(E_FieldOfView))]
public class E_FieldOfViewEditor : Editor
{

    void OnSceneGUI()
    {
        E_FieldOfView targetLight = (E_FieldOfView)target;
        Handles.color = Color.yellow;
        Handles.DrawWireArc(targetLight.transform.transform.Find("Eye").position, Vector3.forward, Vector3.right, 360, targetLight.Distance);
    }

}
