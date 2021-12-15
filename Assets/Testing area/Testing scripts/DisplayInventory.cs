using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    public GameObject inventoryPrefab;
    public InventoryObject inventory;
    public int XStart;
    public int YStart;
    public int XSpaceBetweenItem;
    public int NumberOfColumn;
    public int YSpaceBetweenItems;

    Dictionary<GameObject ,InventorySlot> itemsDisplayed = new Dictionary<GameObject ,InventorySlot>();
    
    void Start(){
        CreateSlots();
    }
    
    void Update(){
        UpdateSlots();
    }

    public void UpdateSlots(){
        foreach (KeyValuePair<GameObject, InventorySlot> slot in itemsDisplayed){
            if (slot.Value.ID >= 0){
                slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite =
                    inventory.database.GetItem[slot.Value.item.Id].uiDisplay;
                slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                slot.Key.GetComponentInChildren<TextMeshProUGUI>().text =
                    slot.Value.amount == 1 ? "" : slot.Value.amount.ToString("n0");
            }
            else{
                slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }
    
    public void CreateSlots(){
        itemsDisplayed = new Dictionary<GameObject ,InventorySlot>();
        for (int i = 0; i < inventory.Container.Items.Length; i++){
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            
            itemsDisplayed.Add(obj, inventory.Container.Items[i]);
        }
    }
    
    public Vector3 GetPosition(int i){ //Gets the position of item to the inventory space
        return new Vector3(XStart+ (XSpaceBetweenItem * (i % NumberOfColumn)), (YStart + (-YSpaceBetweenItems * (i/NumberOfColumn))), 0f);
    }
}
