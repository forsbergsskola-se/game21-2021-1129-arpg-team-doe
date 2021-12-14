using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "New Leveling System", menuName = "Leveling System/Leveling System")]
public class LevelingGameObject : ScriptableObject
{
    [Min(0)][SerializeField] int level = 1;
    [Min(0)][SerializeField] int currentXP = 0;

    [Min(0)][SerializeField] int requiredXPInt; //Used for display and starting value
    float requiredXPFloat; //Used for calculations

    [Tooltip("How much more xp you'll need each level to level up")]
    [Min(0)][SerializeField] float xpScale;

    [SerializeField] GameEvent _levelUp;

    void OnEnable(){
        requiredXPFloat = requiredXPInt;
    }

    public void ReceiveXP(int XP){
        currentXP += XP;
        CheckLevelUp();
    }

    //We currently have 55 xp
    //Killing an enemy -
    //Gives 10 xp

    public void CheckLevelUp(){ //Every time we get xp, run this method
        if (currentXP >= requiredXPInt){
            currentXP -= requiredXPInt;
            level++;
            requiredXPFloat *= xpScale;
            requiredXPInt = (int)requiredXPFloat;
        }
    }
}
