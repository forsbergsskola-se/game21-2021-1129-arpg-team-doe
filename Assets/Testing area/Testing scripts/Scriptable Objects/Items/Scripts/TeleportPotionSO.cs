using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Teleport Potion", menuName = "Inventory System/Items/Consumable/Teleport Potion")]
public class TeleportPotionSO : ConsumableObject
{

    [SerializeField] public Vector3 targetTeleportLocation;
    [Min(0f)][Tooltip("Duration in Seconds")] public int durationUntilTeleport;
    [Min(0f)][Tooltip("Duration in Seconds")] public int toxicityDuration;
    
}
