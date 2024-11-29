using System.Collections;
using CustomLogs;
using UnityEngine;

public interface IConsumable{
    public void Consume(){
    }
}
public class Consumer : MonoBehaviour, IConsumable
{
    //How it should work
    // * If right clicking on consumable object in inventory, set that object as _consumableObject
    // * Then call that consumable objects use function which holds the consume method.
    
    [HideInInspector] public ConsumableObject _consumableObject;
    [SerializeField] int maxToxicityLevel = 3;
    public Statistics _statistics;
    public Health _health;
    int toxicityLevel;
    
    UIStats[] _uiStatsArray;

    void Awake(){
        _statistics = GetComponent<Statistics>();
        _health = GetComponent<Health>();
        _uiStatsArray = FindObjectsOfType<UIStats>();
    }

    public void Consume(){
        if (_consumableObject is StatBuffObject){
            ConsumeItem((StatBuffObject)_consumableObject);
        }

        if (_consumableObject is HealingObject){
            ConsumeItem((HealingObject)_consumableObject);
        }

        if (_consumableObject is TeleportPotionSO)
        {
            ConsumeItem((TeleportPotionSO)_consumableObject);
        }
    }

    [ContextMenu("ConsumeItem")]
     void ConsumeItem(HealingObject consumedItem){
        if (_consumableObject == null){
            return;
        }

        if (toxicityLevel < maxToxicityLevel){
            StartCoroutine(AddHealthCoroutine(consumedItem));
        }
     }

    void ConsumeItem(StatBuffObject consumedItem){
        if (_consumableObject == null){
            return;
        }
        if (toxicityLevel < maxToxicityLevel){
            StartCoroutine(AddStatsCoroutine(consumedItem));
        }
    }
    
     void ConsumeItem(TeleportPotionSO consumedItem){
        if (_consumableObject == null){
            return;
        }
        if (toxicityLevel < maxToxicityLevel){
            StartCoroutine(TeleportToTargetCoroutine(consumedItem));
        }
     }

    IEnumerator AddStatsCoroutine(StatBuffObject consumedItem){
       
        consumedItem.PlayConsumeSound();
        _statistics.AddStats(consumedItem.toughnessBuff,consumedItem.strengthBuff,consumedItem.dexterityBuff,consumedItem.knowledgeBuff,consumedItem.reflexBuff,consumedItem.luckBuff);
        UpdateUI();
        toxicityLevel += consumedItem.toxicityAmount;
        yield return new WaitForSeconds(consumedItem.buffDuration);
        _statistics.AddStats(-consumedItem.toughnessBuff,-consumedItem.strengthBuff,-consumedItem.dexterityBuff,-consumedItem.knowledgeBuff,-consumedItem.reflexBuff,-consumedItem.luckBuff);
        toxicityLevel -= consumedItem.toxicityAmount;
        _consumableObject = null;
        UpdateUI();
        //this.Log(toxicityLevel);
    }
    
    IEnumerator AddHealthCoroutine(HealingObject consumedItem){
        //this.Log("Consumed Health Potion for" + consumedItem.restoreHealthValue);
        consumedItem.PlayConsumeSound();
        _health.UpdateHealth(consumedItem.restoreHealthValue);
        toxicityLevel += consumedItem.toxicityAmount;
        yield return new WaitForSeconds(consumedItem.toxicityDuration);
        toxicityLevel -= consumedItem.toxicityAmount;
        _consumableObject = null;
        //this.Log(toxicityLevel);
    }
    
    IEnumerator TeleportToTargetCoroutine(TeleportPotionSO consumedItem){
        //this.Log("Starts Teleporting");
        consumedItem.PlayConsumeSound();
        yield return new WaitForSeconds(consumedItem.durationUntilTeleport);
        gameObject.transform.position = consumedItem.targetTeleportLocation;
        toxicityLevel += consumedItem.toxicityAmount;
        yield return new WaitForSeconds(consumedItem.toxicityDuration);
        toxicityLevel -= consumedItem.toxicityAmount;
        _consumableObject = null;
        //this.Log(toxicityLevel);
    }

    void UpdateUI()
    {
        foreach (var UIStat in _uiStatsArray)
        {
            UIStat.UpdateAllUIText();
        }
    }
}
