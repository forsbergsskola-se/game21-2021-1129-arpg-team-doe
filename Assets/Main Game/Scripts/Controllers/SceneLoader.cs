using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;
using STOP_MODE = FMODUnity.STOP_MODE;

[RequireComponent(typeof(InteractableObject))]
[DisallowMultipleComponent]
public class SceneLoader : MonoBehaviour,Iinteractable
{
FMOD.Studio.Bus bus;

void Start()
{
    bus = FMODUnity.RuntimeManager.GetBus("bus:/Main Bus");
}

public void Use(){
        Invoke(nameof(LoadScene), 1f);
    }

    public void LoadScene()
    {
        // player.SetActive(false);
        SceneManager.LoadSceneAsync("Scene2");
        bus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        StartCoroutine(StopMusic());
    }

    IEnumerator StopMusic()
    {
        bus.getVolume(out float volume);
        bus.setVolume(0);
        yield return new WaitForSeconds(1);
        bus.setVolume(volume);
    }
    
    
}
