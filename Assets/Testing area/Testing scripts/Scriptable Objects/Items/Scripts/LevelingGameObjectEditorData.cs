using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Leveling Game Object Data", menuName = "Data/Leveling Game Object Data")]

public class LevelingGameObjectEditorData : ScriptableObject
{
   [SerializeField] public int level = 1;
   [SerializeField] public int currentXP = 0;
   [SerializeField] public int requiredXPInt = 0;
   [SerializeField] public float xpScale = 1;
}
