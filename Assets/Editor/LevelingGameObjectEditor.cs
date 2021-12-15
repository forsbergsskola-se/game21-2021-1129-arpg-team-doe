using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(LevelingGameObject))]
public class LevelingGameObjectEditor : Editor
{ 
    [SerializeField] LevelingGameObjectEditorData _data;
    public override void OnInspectorGUI(){
        LevelingGameObject _levelingGameObject = (LevelingGameObject)target;
        base.OnInspectorGUI();
        using (new GUILayout.HorizontalScope()){
           
            if (GUILayout.Button("Save Values")){
                _data.level = _levelingGameObject.level;
                _data.currentXP = _levelingGameObject.currentXP;
                _data.requiredXPInt = _levelingGameObject.requiredXPInt;
                _data.xpScale = _levelingGameObject.xpScale;
            }
            if (GUILayout.Button("Apply Values")){
                 _levelingGameObject.level = _data.level;
                 _levelingGameObject.currentXP = _data.currentXP;
                 _levelingGameObject.requiredXPInt = _data.requiredXPInt;
                 _levelingGameObject.xpScale = _data.xpScale;
            }
        }
        
    }
}
