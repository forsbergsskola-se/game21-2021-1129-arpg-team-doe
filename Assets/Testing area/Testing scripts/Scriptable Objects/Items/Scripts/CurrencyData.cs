using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


[CreateAssetMenu(fileName = "New Currency Data", menuName = "Inventory System/Items/Currency Data")]
public class CurrencyData : ItemObject{
    public FMODUnity.EventReference fmodEvent;
    [SerializeField] int minAmount;
    [SerializeField] int maxAmount;

    void Awake(){
        type = ItemType.Currency;
    }

    public int GenerateValue(){
        return Random.Range(minAmount, maxAmount);
    }
}
