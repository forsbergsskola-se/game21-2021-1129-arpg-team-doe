using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour{
    [SerializeField] CurrencyData _currencyData;

    void OnCollisionEnter(Collision other){
        if (other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<CurrencyHolder>()._currencyHolderDataSo.AddCurrency(_currencyData.GenerateValue());
            Destroy(this.gameObject);
        }
    }
}
