using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

[CreateAssetMenu]
public class LootTable : ScriptableObject{
    
    [Serializable]
    public class Drop{
        public GameObject drop;
        public int dropWeight;
    }

    Statistics _playerStatistics;
    
    int rollCount;

    public List<Drop> table;
    
    [NonSerialized]
    int totalWeight = -1;

    public int TotalWeight{
        get{
            if (totalWeight == -1){
                CalculateTotalWeight();
            }

            return totalWeight;
        }
    }

    void CalculateTotalWeight(){
        totalWeight = 0;
        for (int i = 0; i < table.Count; i++){
            totalWeight += table[i].dropWeight;
        }
    }

    public GameObject GetDrop(){
        int maxRollCount = 2;
        
        int roll = UnityEngine.Random.Range(0, TotalWeight);

        for (int i = 0; i < table.Count; i++){
            roll -= table[i].dropWeight;
            
            if (roll < 0){
                return table[i].drop;
            }
        }

        return table[0].drop;
    }
    
    public GameObject GetDropCash(){
        int maxRollCount = 2;


        if (rollCount < maxRollCount){
            int roll = UnityEngine.Random.Range(0, TotalWeight);

        

            for (int i = 0; i < table.Count; i++){
                roll -= table[i].dropWeight;
            
                if (roll < 0){
                    return table[i].drop;
                }

                rollCount++;
            }
            if (UnityEngine.Random.Range(0f,1f) < _playerStatistics.Luck){
                GetDropCash();
                //PlayLuckSound();
            }
            return table[0].drop;
        }
        return null;
    }
}
