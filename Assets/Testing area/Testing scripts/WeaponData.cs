using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponData : ScriptableObject
{
    public GameObject weaponPrefab;
    public string weaponName;
    public int damage;
    public int toughness;
    public int strength;
    public int dexterity;
    public int knowledge;
    public int luck;
}
