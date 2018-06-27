using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(S_SoundRadius))]
public class S_SoundRadiusEditor : Editor
{

    void OnSceneGUI()
    {
        S_SoundRadius targetSound = (S_SoundRadius)target;
        Handles.color = Color.yellow;
        Handles.DrawWireArc(targetSound.transform.position, Vector3.forward, Vector3.right, 360, targetSound.s_soundRadius);

    }

}
