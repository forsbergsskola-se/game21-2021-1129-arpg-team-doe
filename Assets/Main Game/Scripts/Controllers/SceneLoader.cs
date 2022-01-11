using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;
using STOP_MODE = FMODUnity.STOP_MODE;

//[RequireComponent(typeof(InteractableObject))]
[DisallowMultipleComponent]
public class SceneLoader : MonoBehaviour,Iinteractable
{
    [SerializeField] Texture2D defaultCursor;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.tag == "Player")
        {
            Invoke(nameof(LoadScene), 0.3f);
        }
    }

    void LoadScene()
    { 
        Cursor.SetCursor(defaultCursor, Vector2.zero , CursorMode.ForceSoftware);
        SceneManager.LoadScene("Scene2");
    }


    // FMOD.Studio.Bus bus;
//
// void Start()
// {
//     bus = FMODUnity.RuntimeManager.GetBus("bus:/Main Bus");
// }
//



//
// public void Use(){
//         Invoke(nameof(LoadScene), 0.3f);
//     }
//
//     public void LoadScene()
//     {
//         // player.SetActive(false);
//         //bus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
//         SceneManager.LoadScene("Scene2");
//        // StartCoroutine(StopMusic());
//     }
//
//     IEnumerator StopMusic()
//     {
//         bus.getVolume(out float volume);
//         Debug.Log(volume);
//         bus.setVolume(0);
//         yield return new WaitForSeconds(0.5f);
//         bus.setVolume(1);
//     }
    
    
}
