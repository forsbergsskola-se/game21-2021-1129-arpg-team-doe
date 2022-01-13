using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEditor;
using UnityEngine;

public class SecretEditor : EditorWindow{

    [MenuItem("Secret Tools/Secret Window")]
    public static void OpenWindow() => GetWindow<SecretEditor>("Secret Window");

    void OnGUI(){
        GUILayout.Label("Do not press");
        if (GUILayout.Button("Dare to press?")){
            //Insert function here
        }
    }
}
