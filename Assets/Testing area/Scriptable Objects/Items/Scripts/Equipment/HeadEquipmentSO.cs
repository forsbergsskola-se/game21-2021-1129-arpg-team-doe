using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Equipment/Head")]
public class HeadEquipmentSO : EquipmentObject
{
    public void Awake(){
        equipmentType = EquipmentType.Head;
    }
}
