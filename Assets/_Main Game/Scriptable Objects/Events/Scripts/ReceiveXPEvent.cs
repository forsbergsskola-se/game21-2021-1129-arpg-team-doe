using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Receive XP Event",menuName = "Receive XP Event")]

public class ReceiveXPEvent : ScriptableObject{
    HashSet<ReceiveXPEventListener> _listeners = new HashSet<ReceiveXPEventListener>();

    XPDrop _xpDrop;

    public void Invoke(){
        foreach (var globalEventListener in _listeners){
            globalEventListener.RaiseEvent();
        }
    }

    public void Register(ReceiveXPEventListener receiveXpEventListener) => _listeners.Add(receiveXpEventListener);
    public void Deregister(ReceiveXPEventListener receiveXpEventListener) => _listeners.Remove(receiveXpEventListener);
}
