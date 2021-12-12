using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(ConsumableObject))]
public class ConsumableObjectEditor : Editor{
    public override void OnInspectorGUI(){
         
        ConsumableObject _consumableObject = (ConsumableObject)target;
        base.OnInspectorGUI();
        using (new GUILayout.HorizontalScope()){
            if (GUILayout.Button("Reset All Buff Values")){
                _consumableObject.toughnessBuff = 0;
                _consumableObject.strengthBuff = 0;
                _consumableObject.dexterityBuff = 0;
                _consumableObject.knowledgeBuff =0;
                _consumableObject.luckBuff = 0;
                _consumableObject.attackSpeedBuff = 0;
                _consumableObject.damageBuff = 0;
                
                _consumableObject.buffDuration = 0;
            }
            if (GUILayout.Button("Randomize All Buff Values")){
                _consumableObject.toughnessBuff = Random.Range(0, 6);
                _consumableObject.strengthBuff = Random.Range(0, 6);
                _consumableObject.dexterityBuff = Random.Range(0, 6);
                _consumableObject.knowledgeBuff = Random.Range(0, 6);
                _consumableObject.luckBuff = Random.Range(0, 6);
                _consumableObject.attackSpeedBuff = Random.Range(0, 1);
                _consumableObject.damageBuff = Random.Range(0, 6);
                
                _consumableObject.buffDuration = Random.Range((Random.Range(30, 61)),Random.Range(60, 121));
                
            }
        }
        

        
        
    }
}