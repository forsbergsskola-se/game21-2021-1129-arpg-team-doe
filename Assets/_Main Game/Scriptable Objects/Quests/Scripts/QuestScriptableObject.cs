using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest",menuName = "Quest")]
public class QuestScriptableObject : ScriptableObject{
    [Header("Quest Description")]
    [SerializeField] [TextArea (10,10)] public string description;

    [Header("Quest Rewards")] 
    [SerializeField] float moneyReward; //TODO: Rename this

    [SerializeField] int xpReward; //TODO: Check up on this

    [SerializeField]
    List<QuestObjectivesSO> _questObjectives;
}
