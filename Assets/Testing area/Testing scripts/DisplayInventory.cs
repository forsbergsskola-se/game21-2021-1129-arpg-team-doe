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

    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    
    void Start(){
        CreateDisplay();
    }
    
    void Update(){
        UpdateDisplay();
    }
    
    public void UpdateDisplay(){
        for (int i = 0; i < inventory.Container.Items.Count; i++){
            InventorySlot slot = inventory.Container.Items[i];
            if (itemsDisplayed.ContainsKey(slot)){
                itemsDisplayed[slot].GetComponentInChildren<TextMeshProUGUI>().text =
                    slot.amount.ToString("n0");
            }
            else{
                var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
                obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[slot.item.Id].uiDisplay;
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
                itemsDisplayed.Add(slot, obj);
            }
        }
    }
    
    public void CreateDisplay(){ 
        for (int i = 0; i < inventory.Container.Items.Count; i++){
            InventorySlot slot = inventory.Container.Items[i];
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
            itemsDisplayed.Add(slot, obj);
        }
    }
    
    public Vector3 GetPosition(int i){ //Gets the position of item to the inventory space
        return new Vector3(XStart+ (XSpaceBetweenItem * (i % NumberOfColumn)), (YStart + (-YSpaceBetweenItems * (i/NumberOfColumn))), 0f);
    }
}
