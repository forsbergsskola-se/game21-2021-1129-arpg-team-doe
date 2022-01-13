using UnityEngine;
[CreateAssetMenu(fileName = "New Healing Object", menuName = "Inventory System/Items/Consumable/Healing Object")]
public class HealingObject : ConsumableObject{
    public int restoreHealthValue;
    public int toxicityDuration;
}
