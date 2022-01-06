using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Drop XP Event",menuName = "Drop XP Event")]

public class XPDropEvent : ScriptableObject
{
    HashSet<XPDropEventListener> _listeners = new HashSet<XPDropEventListener>();

    XPDrop _xpDrop;

    public void Invoke(int xpAmount){
        foreach (var globalEventListener in _listeners){
            globalEventListener.RaiseEvent(xpAmount);
        }
    }

    public void Register(XPDropEventListener xpDropEventListener) => _listeners.Add(xpDropEventListener);
    public void Deregister(XPDropEventListener xpDropEventListener) => _listeners.Remove(xpDropEventListener);
}
