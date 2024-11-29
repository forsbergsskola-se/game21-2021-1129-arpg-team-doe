using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class RefinedObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public int initialSize;
    public bool dontDestroyOnLoad;

    List<GameObject> pooledItems;
    List<GameObject> activeItems;

    void Start() {
        for (int i = 0; i < this.initialSize; i++) {
            CreateInstance();
        }

        if (this.dontDestroyOnLoad) {
            DontDestroyOnLoad(this);
        }
    }

    public void Prepare(int amount) {
        for (int i = this.pooledItems.Count; i < amount; i++) {
            CreateInstance();
        }
    }

    public GameObject Get() {
        if (this.pooledItems.Count == 0) {
            // recycle:
            Return(this.activeItems[0]);
        }
        var instance = this.pooledItems[this.pooledItems.Count-1];
        this.pooledItems.RemoveAt(this.pooledItems.Count-1);
        this.activeItems.Add(instance);
        instance.SetActive(true);
        return instance;
    }
    
    public void Return(GameObject instance) {
        this.activeItems.Remove(instance);
        instance.SetActive(false);
        this.pooledItems.Add(instance);
    }

    void CreateInstance() {
        var instance = Instantiate(this.prefab);
        Return(instance);
    }
}