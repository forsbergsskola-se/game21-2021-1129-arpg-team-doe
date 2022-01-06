using UnityEngine;
[CreateAssetMenu(fileName = "New Feet Object", menuName = "Inventory System/Items/Equipment/Feet")]
public class FeetEquipmentGearSO : EquipmentObject{
    public void Awake(){
        equipmentType = EquipmentType.Feet;
    }
}
