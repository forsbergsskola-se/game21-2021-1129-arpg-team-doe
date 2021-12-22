using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyHolder : MonoBehaviour
{
    [SerializeField] public CurrencyHolderDataSO _currencyHolderDataSo;
    
    //Sound
    [SerializeField] public FMODUnity.EventReference fmodEvent;
    FMOD.Studio.EventInstance instance;
    void Start(){
        if (!fmodEvent.IsNull){
            instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        }
    }
    public void PlaySound(){
        instance.start();
        instance.release();
    }
    
}
