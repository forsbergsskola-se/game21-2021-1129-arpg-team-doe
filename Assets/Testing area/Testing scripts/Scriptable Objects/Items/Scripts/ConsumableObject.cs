using Unity.VisualScripting;

public abstract class ConsumableObject : ItemObject, IConsumable{
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
}
