using UnityEngine;
[CreateAssetMenu(fileName = "New Chest Object", menuName = "Inventory System/Items/Equipment/Chest")]
public class ChestEquipmentGearSO : EquipmentObject{
    public void Awake(){
        equipmentType = EquipmentType.Chest;
    }
}
