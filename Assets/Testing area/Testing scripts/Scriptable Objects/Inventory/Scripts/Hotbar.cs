using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    void Awake(){
        foreach (var button in GetComponentsInChildren<HotbarButton>()){
            button.OnButtonClicked += ButtonOnOnButtonClicked;
        }
    }

    void ButtonOnOnButtonClicked(int buttonNumber){
        Debug.Log($"Button {buttonNumber} clicked!");
    }
}
