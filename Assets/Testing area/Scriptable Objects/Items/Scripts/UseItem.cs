using UnityEngine;

public class UseItem : MonoBehaviour{
    ItemObject _itemObject;

    void Update(){
        if (Input.GetMouseButtonDown(1)){
            _itemObject.UseItem();
        }
    }
}
