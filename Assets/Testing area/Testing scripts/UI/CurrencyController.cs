using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyController : MonoBehaviour{
   [SerializeField] CurrencyHolderSO playerCurrencySo;
   [SerializeField] TextMeshProUGUI textMesh;
   [SerializeField] string currencyName;
   

   void Start(){
      UpdateCurrency();
   }

   public void UpdateCurrency(){
      textMesh.text = $"{currencyName}: {playerCurrencySo.currency}";
   }
}
