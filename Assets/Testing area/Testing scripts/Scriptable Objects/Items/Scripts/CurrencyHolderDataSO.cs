using UnityEngine;
[CreateAssetMenu(fileName = "New Currency Holder", menuName = "Inventory System/Currency Holder")]
public class CurrencyHolderDataSO : ScriptableObject{ //Wallet
   [Min(0)][SerializeField] int currency;
   [SerializeField] GameEvent currencyChangeEvent;
  
   
   public int Currency{
      get => currency;
      private set => currency = value;
   }

   public void AddCurrency(int amount){
      Currency += amount;
      currencyChangeEvent.Invoke();
   }
}
