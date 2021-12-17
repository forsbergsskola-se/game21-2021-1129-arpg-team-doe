using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStatScreen : MonoBehaviour
{
    [SerializeField] Button applyButton;
    
    [ContextMenu("OnDisable")]
    void OnDisable(){
        applyButton.onClick.Invoke();
    }
}
