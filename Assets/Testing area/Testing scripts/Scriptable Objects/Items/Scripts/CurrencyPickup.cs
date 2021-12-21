using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyPickup : MonoBehaviour{
    [SerializeField] CurrencyObject _currencyObject;

    void OnCollisionEnter(Collision other){
        if (other.gameObject.CompareTag("Player")){
            //pickup the money my g
            //Curreny += GoldAmount
            other.gameObject.GetComponent<CurrencyHolderSO>();
        }
    }
}
