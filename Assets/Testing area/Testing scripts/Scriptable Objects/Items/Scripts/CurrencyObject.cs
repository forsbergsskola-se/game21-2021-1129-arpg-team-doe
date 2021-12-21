using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Currency Object", menuName = "Inventory System/Items/Currency")]
public class CurrencyObject : ItemObject{
    [SerializeField] GameEvent _pickupCurrencyEvent;
    
    public FMODUnity.EventReference fmodEvent;
    public int currencyAmount;

    void Awake(){
        type = ItemType.Currency;
    }
}
