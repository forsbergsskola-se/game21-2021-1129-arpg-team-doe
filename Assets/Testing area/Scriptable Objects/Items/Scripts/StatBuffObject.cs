using UnityEngine;
[CreateAssetMenu(fileName = "New Stat Buff Object", menuName = "Inventory System/Items/Consumable/Stat Buff Object")]
public class StatBuffObject : ConsumableObject{
    [Header("Attribute Buffs")]
    [Min(0f)][Tooltip	("Duration in Seconds")] public int buffDuration;
    public float toughnessBuff;
    public float strengthBuff;
    public float dexterityBuff;
    public float knowledgeBuff;
    public float reflexBuff;
    public float luckBuff;
}
