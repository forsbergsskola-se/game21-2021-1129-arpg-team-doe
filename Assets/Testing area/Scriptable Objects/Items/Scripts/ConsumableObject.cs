using FMOD.Studio;
using FMODUnity;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ConsumableObject : ItemObject, IConsumable{
    [SerializeField] FMODUnity.EventReference consumeSound;
    
    EventInstance consumeSoundInstance;
    
    bool hasFoundConsumeSound;
    
    public int toxicityAmount;
    
    
    

    public void Awake() {
       this.GameObject();
       type = ItemType.Consumable;
   }

   public override void UseItem(){ //When used in inventory
       Consume();
   }

   public virtual void Consume(){
       
   }
   
   
   public virtual void PlayConsumeSound(){
       hasFoundConsumeSound = !consumeSound.IsNull;
       if (!hasFoundConsumeSound)
       {
           consumeSound = EventReference.Find("event:/HpPotion");
       }
       consumeSoundInstance = RuntimeManager.CreateInstance(consumeSound);
       consumeSoundInstance.start();
       consumeSoundInstance.release();
   }
}
