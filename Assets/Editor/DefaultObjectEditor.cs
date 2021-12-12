using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(DefaultObject))]
public class DefaultObjectEditor : Editor{
    public override void OnInspectorGUI(){
         
        DefaultObject _defaultObject = (DefaultObject)target;
        base.OnInspectorGUI();
        using (new GUILayout.HorizontalScope()){
            if (GUILayout.Button("Reset All Values")){
                _defaultObject.price = 0;
                _defaultObject.weight = 0;
            }
            if (GUILayout.Button("Randomize All Values")){
                _defaultObject.price = Random.Range(1, 30);
                _defaultObject.weight = Random.Range(1, 100);
            }
        }
        

        
        
    }
}
