using UnityEngine;

public class SoundIncrement : MonoBehaviour{
    SoundControl _soundControl;

    void Start(){
        _soundControl = FindObjectOfType<SoundControl>();
    }

    public void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            _soundControl.SoundIncrement++;
            _soundControl.Progress(); 
            Destroy(gameObject);
        }
    }
}
