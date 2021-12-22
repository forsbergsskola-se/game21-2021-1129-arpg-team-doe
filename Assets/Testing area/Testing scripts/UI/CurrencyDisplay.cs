using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyDisplay : MonoBehaviour{
   [SerializeField] CurrencyHolderDataSO playerCurrencyHolderDataSo;
   [SerializeField] TextMeshProUGUI currencyText;
   [SerializeField] string currencyName;
   

   void Start(){
      UpdateCurrency();
   }

   public void UpdateCurrency(){
      currencyText.text = $"{currencyName}: {playerCurrencyHolderDataSo.Currency}";
   }
}
