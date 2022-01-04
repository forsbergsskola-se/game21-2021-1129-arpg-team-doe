using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Currency : MonoBehaviour{
    [SerializeField] CurrencyData currencyData;
    internal int amount;

    void Start(){
        GenerateValue();
    }

    void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<CurrencyHolder>()._currencyHolderDataSo.AddCurrency(amount);
            Destroy(gameObject);
        }
    }
    void GenerateValue(){
        amount = Random.Range(currencyData.minAmount, currencyData.maxAmount);
        //Debug.Log(amount);
        //return amount;
    }
}
