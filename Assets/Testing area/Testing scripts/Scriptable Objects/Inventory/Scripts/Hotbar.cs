using UnityEngine;

public class HotBar : MonoBehaviour
{
    void Awake(){
        foreach (var button in GetComponentsInChildren<HotbarButton>()){
            button.OnButtonClicked += ButtonOnOnButtonClicked;
        }
    }

    void ButtonOnOnButtonClicked(int buttonNumber){
        //Debug.Log($"Button {buttonNumber} clicked!");
    }
}
