using TMPro;
using UnityEngine;

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
