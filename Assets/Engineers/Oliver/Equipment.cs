using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class Equipment : ScriptableObject{
   [SerializeField] UnityEvent DoStuff;

   public string name;
   public Sprite sprite;
   public int damage;
   public int toughness;
   public int strength;
   public int dexterity;
   public int knowledge;
   public int luck;
}
