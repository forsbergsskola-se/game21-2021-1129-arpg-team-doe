using FMOD.Studio;
using FMODUnity;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ConsumableObject : ItemObject, IConsumable{
    [SerializeField] EventReference consumeSound;
    
    EventInstance _consumeSoundInstance;

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
       _consumeSoundInstance = RuntimeManager.CreateInstance("event:/HpPotion");
       if (!consumeSound.IsNull){
           _consumeSoundInstance = RuntimeManager.CreateInstance(consumeSound);
       }
       _consumeSoundInstance.start();
       _consumeSoundInstance.release();
   }
}
