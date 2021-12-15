using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterZone : MonoBehaviour{
    [SerializeField] XPDrop _xpDrop;

    void Start(){
        if (_xpDrop != null){
            _xpDrop = GetComponent<XPDrop>();
        }
    }

    public void OnTriggerEnter(Collider other){
        if (other.tag == "Player"){
            if (_xpDrop != null){
                _xpDrop._xpDropEvent.Invoke(_xpDrop.xpAmount);
                _xpDrop = null;
            }
            
            
        }
    }
}
