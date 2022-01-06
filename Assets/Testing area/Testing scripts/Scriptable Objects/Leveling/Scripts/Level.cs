using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] public LevelingGameObject LevelingGameObject;
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
