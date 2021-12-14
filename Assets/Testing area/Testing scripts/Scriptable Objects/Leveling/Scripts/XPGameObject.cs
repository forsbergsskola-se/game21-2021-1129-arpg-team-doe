using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "New Leveling System", menuName = "Leveling System")]
public class XPGameObject : ScriptableObject
{
    [SerializeField] int level = 1;
    [SerializeField] int currentXP = 0;
    
    [SerializeField] int requiredXPInt; //Used for display and starting value
    float requiredXPFloat; //Used for calculations
    
    [Tooltip("How much more xp you'll need each level to level up")]
    [SerializeField] float xpScale;

    [SerializeField] GameEvent _levelUp;


    void Awake(){
        requiredXPFloat = requiredXPInt;
    }
    
    //We currently have 55 xp
    //Killing an enemy -
    //Gives 10 xp
    
    


    public void CheckLevelUp(){ //Every time we get xp, run this method
        if (currentXP >= requiredXPFloat){
            currentXP -= (int)requiredXPFloat;
            level++;
            requiredXPFloat *= xpScale;
            requiredXPInt = (int)requiredXPFloat;
        }
    }
    
    
    
    
}
