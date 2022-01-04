using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


[CreateAssetMenu(fileName = "New Currency Data", menuName = "Inventory System/Items/Currency Data")]
public class CurrencyData : ItemObject{
    [SerializeField] internal int minAmount;
    [SerializeField] internal int maxAmount;
    

    void Awake(){
        type = ItemType.Currency;
    }
}
