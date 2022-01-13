using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HelpMenu : MonoBehaviour{
    [SerializeField]GameObject helpTextGameObject;
    
    void Update(){
        if (Input.GetKeyDown(KeyCode.H)){
            ToggleHelpMenu();
        }
    }

    void ToggleHelpMenu(){
        if (helpTextGameObject.activeInHierarchy == false){
            helpTextGameObject.SetActive(true);
        }
        else{
            helpTextGameObject.SetActive(false);
        }
    }
}
