using UnityEngine;

public class EnterZone : MonoBehaviour{
    [SerializeField] XPDrop _xpDrop;
    
    public void OnTriggerEnter(Collider other){
        if (other.tag == "Player"){
            if (_xpDrop != null){
               _xpDrop._xpDropEvent.Invoke(_xpDrop.xpAmount);
               _xpDrop = null;
            }
        }
    }
}
