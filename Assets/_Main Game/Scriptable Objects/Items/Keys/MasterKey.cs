using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterKey : MonoBehaviour
{
    [SerializeField]List<UnlockDoor> unlockableDoors;

    void Start()
    {
        foreach (var unlockDoor in  FindObjectsOfType<UnlockDoor>())
        {
            unlockableDoors.Add(unlockDoor);
        }
    }

    void OnDisable()
    {
        unlockableDoors.Clear();
    }

    public void UseItem()
    {
        foreach (var unlockableDoor in unlockableDoors)
        {
            unlockableDoor.GetComponent<UnlockDoor>().UnlockDoorWithKey();
        }
        Debug.Log("Used Key");
        Destroy(this.gameObject);
    }


}
