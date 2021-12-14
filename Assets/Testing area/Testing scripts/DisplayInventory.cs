using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayInventory : MonoBehaviour{
    public InventoryObject inventory;


    public int XStart;
    public int YStart;
    public int XSpaceBetweenItem;
    public int NumberOfColumn;
    public int YSpaceBetweenItems;

    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    // Start is called before the first frame update
    void Start(){
        CreateDisplay();
    }

    // Update is called once per frame
    void Update(){
        //UpdateDisplay();
    }

    public void CreateDisplay(){
        for (int i = 0; i < inventory.Container.Count; i++){
            var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
        }
    }

    public Vector3 GetPosition(int i){
        return new Vector3(XStart+ (XSpaceBetweenItem * (i % NumberOfColumn)), (YStart + (-YSpaceBetweenItems * (i/NumberOfColumn))), 0f);
    }
}
