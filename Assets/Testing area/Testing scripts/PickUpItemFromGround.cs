using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PickUpItemFromGround : MonoBehaviour
{
    InventoryController _inventoryController;
   [HideInInspector] public GameObject pickedUpTarget;
    Vector3 savedPosition;
   [SerializeField]  ItemGrid _itemGrid;


    void Start()
    {
        _inventoryController = FindObjectOfType<InventoryController>();
    }

    void Update()
    { 
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftControl)) {
            if (CanPickUpCheck()) { 
                pickedUpTarget.GetComponent<PickUpItem>().Use();
            }
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            if (CanPickUpCheck())
            {
                savePickUpTargetPosition();
                PickUpObject();
                //Pick up item
                //Object follows mouse cursor
                //If object is in inventory //function here
            }
            
        }

        

        if (!Input.GetMouseButtonUp(0) && pickedUpTarget != null)
        {
            
            PickUpObject();
        }

        if (Input.GetMouseButtonUp(0))
        {
        
            DropItem();
            //Drop item, if mouse is over ground, drop on ground, if over inventory,
            //drop it in the inventory
            
        }
    }
    
    
    bool CanPickUpCheck(){
        RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
        foreach (RaycastHit hit in hits){
            pickedUpTarget = hit.transform.GetComponent<PickUpItem>()?.gameObject;
            if (pickedUpTarget == null) continue;
            if (pickedUpTarget != null)
            {
               return true; 
            }
            
        }
        return false;
    }
    
    static Ray GetMouseRay(){
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }

    void PickUpObject()
    {Ray ray = GetMouseRay();
             bool hasHit= Physics.Raycast(ray, out RaycastHit hit,10000,LayerMask.GetMask("Ground","UI"));
             //Debug.Log("Picking up object");
        pickedUpTarget.transform.position = hit.point;
        pickedUpTarget.GetComponent<Collider>().enabled = false;
        pickedUpTarget.GetComponent<NavMeshObstacle>().enabled = false;


    }

    void savePickUpTargetPosition()
    {
        savedPosition = pickedUpTarget.transform.position;
    }

    void DropItem()
    {
        pickedUpTarget.GetComponent<Collider>().enabled = true;
        pickedUpTarget.GetComponent<NavMeshObstacle>().enabled = true;
        pickedUpTarget.transform.position = new Vector3(pickedUpTarget.transform.position.x, pickedUpTarget.transform.position.y + 1, pickedUpTarget.transform.position.z);
        if (_itemGrid.IsOutOfInventoryGrid((int)Input.mousePosition.x, (int)Input.mousePosition.y))
        {
            pickedUpTarget = null;
        }
        else if (!_itemGrid.IsOutOfInventoryGrid((int) Input.mousePosition.x, (int) Input.mousePosition.y))
        {
            if (pickedUpTarget.GetComponent<InventoryItem>())
            {
                var pickUpTargetInventoryItem = pickedUpTarget.GetComponent<InventoryItem>();
                _itemGrid.PlaceItem(pickUpTargetInventoryItem,(int)Input.mousePosition.x,(int)Input.mousePosition.y);
            }
            
        }
        
        //If mouse cursor is on ground, drop on ground
        
        
        //Else, if cursor is in inventory, drop on inventory slot, if that slot is empty, and object fits
        //_inventoryController.PlaceItem();
    }
}
