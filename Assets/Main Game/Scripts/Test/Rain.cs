using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(PrefabPooling))]
[DisallowMultipleComponent]
public class Rain : MonoBehaviour
{
    [SerializeField][Range(0,1)] float rainSpawnTime;
    [SerializeField] float spreadRange;
    PrefabPooling _prefabPool;
    bool canRain = true;

    void Start()
    {
        _prefabPool = PrefabPooling.Instance;
    }

    void FixedUpdate()
    {
        // if (canRain)
        // {
        //    StartCoroutine(Raining()); 
        // }
        
         _prefabPool.SpawnFromPool("Rain", transform.position + RandomLocation(), Quaternion.identity);
    }
    Vector3 RandomLocation(){
        //may change x and z between 1-3?
        var position = new Vector3((Random.insideUnitCircle.x * spreadRange), 0f,
            (Random.insideUnitSphere.z * spreadRange));
        return position;
    }

    IEnumerator Raining()
    {
        canRain = false;
       _prefabPool.SpawnFromPool("Rain", transform.position + RandomLocation(), Quaternion.identity);

       yield return new WaitForSeconds(rainSpawnTime);
        canRain = true;
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected(){
        Handles.DrawWireDisc(transform.position, transform.up,spreadRange);
        
    }
#endif
}
