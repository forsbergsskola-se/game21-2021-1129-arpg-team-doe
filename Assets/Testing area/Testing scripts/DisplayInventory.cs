using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour{
    public MouseItem mouseItem = new MouseItem();
    
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
            
            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
            
            itemsDisplayed.Add(obj, inventory.Container.Items[i]);
        }
    }

    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action){
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    public void OnEnter(GameObject obj){
        mouseItem.hoverObj = obj;
        if (itemsDisplayed.ContainsKey(obj)){
            mouseItem.hoverItem = itemsDisplayed[obj];
        }
    }
    public void OnExit(GameObject obj){
        mouseItem.hoverObj = null;
        mouseItem.hoverItem = null;
        
    }
    public void OnDragStart(GameObject obj){
        //Visual representation of draging item
        var mouseObject = new GameObject();
        var rt = mouseObject.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(50, 50);
        mouseObject.transform.SetParent(transform.parent);
        if (itemsDisplayed[obj].ID >= 0){
            var img = mouseObject.AddComponent<Image>();
            img.sprite = inventory.database.GetItem[itemsDisplayed[obj].ID].uiDisplay;
            img.raycastTarget = false; //to make sure mouse ignores object
        }

        mouseItem.obj = mouseObject;
        mouseItem.item = itemsDisplayed[obj];
    }
    public void OnDragEnd(GameObject obj){
        if (mouseItem.hoverObj){
            inventory.MoveItem(itemsDisplayed[obj], itemsDisplayed[mouseItem.hoverObj]);
        }
        else{
            inventory.RemoveItem(itemsDisplayed[obj].item);
        }
        Destroy(mouseItem.obj);
        mouseItem.item = null;
    }
    
    public void OnDrag(GameObject obj){
        if (mouseItem.obj !=null){
            mouseItem.obj.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }
    
    public Vector3 GetPosition(int i){ //Gets the position of item to the inventory space
        return new Vector3(XStart+ (XSpaceBetweenItem * (i % NumberOfColumn)), (YStart + (-YSpaceBetweenItems * (i/NumberOfColumn))), 0f);
    }
}

public class MouseItem{
    public GameObject obj;
    public InventorySlot item;
    public InventorySlot hoverItem;
    public GameObject hoverObj;
}
