using UnityEngine;

public class Hotbar : MonoBehaviour
{
    void Awake(){
        foreach (var button in GetComponentsInChildren<HotbarButton>()){
            button.OnButtonClicked += ButtonOnButtonClicked;
        }
    }

    void ButtonOnButtonClicked(int buttonNumber){
        //Debug.Log($"Button {buttonNumber} clicked!"); 
    }
}
