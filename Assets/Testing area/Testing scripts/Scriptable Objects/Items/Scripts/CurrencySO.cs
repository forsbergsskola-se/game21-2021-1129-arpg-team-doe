using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Currency Holder", menuName = "Inventory System/Currency Holder")]
public class CurrencySO : ScriptableObject{
   [Min(0)][SerializeField] public int currency;
}
