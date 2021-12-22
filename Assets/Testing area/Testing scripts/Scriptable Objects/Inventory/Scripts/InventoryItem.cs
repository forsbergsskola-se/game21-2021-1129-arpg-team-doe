using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour{
   public ItemObject itemObject;
   public int onGridPositionX;
   public int onGridPositionY;
   public bool rotated;

   //for rotating tiles
   public int HEIGHT{
      get{
         if (rotated == false){
            return itemObject.height;
         }
         return itemObject.width;
      }
   }

   public int WIDTH{
      get{
         if (rotated == false){
            return itemObject.width;
         }
         return itemObject.height;
      }
   }
 
   internal void Set(ItemObject itemObjectsasd){
      this.itemObject = itemObjectsasd;
      GetComponent<Image>().sprite = itemObject.itemIcon;
      Vector2 size = new Vector2();
      size.x = itemObject.width * ItemGrid.tileSizeWidth;
      size.y = itemObject.height * ItemGrid.tileSizeHeight;
      GetComponent<RectTransform>().sizeDelta = size;
   }

   internal void Rotate(){
      rotated = !rotated;
      RectTransform rectTransform = GetComponent<RectTransform>();
      rectTransform.rotation = Quaternion.Euler(0, 0, rotated == true ? 90f : 0f);
   }
}