using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Currency Data", menuName = "Inventory System/Items/Currency Data")]
public class CurrencyData : ItemObject{
    public FMODUnity.EventReference fmodEvent;
    public int currencyAmount;
    public int minAmount;
    public int maxAmount;

    void Awake(){
        type = ItemType.Currency;
    }
}
