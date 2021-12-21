using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

[CreateAssetMenu]
public class LootTable : ScriptableObject{
    
    [Serializable]
    public class Drop{
        public ItemObject drop;
        public int weight;
    }

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
            totalWeight += table[i].weight;
        }
    }

    public ItemObject GetDrop(){
        int roll = UnityEngine.Random.Range(0, TotalWeight);

        for (int i = 0; i < table.Count; i++){
            roll -= table[i].weight;
            
            if (roll < 0){
                return table[i].drop;
            }
        }

        return table[0].drop;
    }
}
