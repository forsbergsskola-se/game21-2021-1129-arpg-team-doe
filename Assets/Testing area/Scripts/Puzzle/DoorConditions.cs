using UnityEngine;

public class DoorConditions : MonoBehaviour,Iinteractable 
{
    internal bool Completed;

    public void Use(){
        Completed = true;
    }
}
