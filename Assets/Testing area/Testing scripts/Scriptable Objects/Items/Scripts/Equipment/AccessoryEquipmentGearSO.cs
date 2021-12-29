using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Accessory Object", menuName = "Inventory System/Items/Equipment/Accessory")]
public class AccessoryEquipmentGearSO : EquipmentObject
{
    public void Awake(){
        equipmentType = EquipmentType.Accessory;
    }
}
