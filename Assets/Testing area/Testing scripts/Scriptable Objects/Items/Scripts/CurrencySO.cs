using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Currency", menuName = "Inventory System/Currency")]
public class CurrencySO : ScriptableObject{
   [Min(0)][SerializeField] public int currency;
}
