using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class EquipmentData : ScriptableObject{
   
   public GameObject equipmentPrefab;
   public string equipmentName;
   public int damage;
   public int toughness;
   public int strength;
   public int dexterity;
   public int knowledge;
   public int luck;
}
