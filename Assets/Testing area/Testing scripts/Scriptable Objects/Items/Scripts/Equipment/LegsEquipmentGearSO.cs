using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Legs Object", menuName = "Inventory System/Items/Equipment/Legs")]
public class LegsEquipmentGearSO : EquipmentObject
{
    public void Awake(){
        equipmentType = EquipmentType.Legs;
    }
}
