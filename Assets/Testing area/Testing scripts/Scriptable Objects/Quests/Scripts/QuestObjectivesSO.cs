using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Quest Objective", menuName = "Quest Objective")]
public class QuestObjectivesSO : ScriptableObject
{
    [Header("Quest Objective")][SerializeField] [TextArea (10,10)] public string objectiveDescription;
    [SerializeField] bool objectiveCompleted; //TODO:Serialized used for debugging 
    [SerializeField] bool objectiveFailed; //TODO:Serialized used for debugging 
    
}
