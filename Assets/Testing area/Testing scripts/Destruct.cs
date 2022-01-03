using System;
using UnityEngine;
using UnityEngine.AI;

public interface IDestructible{
    void Destruction(bool isAlive);
}
public class Destruct : MonoBehaviour,IDestructible, IHealthListener{

    //FMODUnity.StudioEventEmitter _destructionEventEmitter;

    [SerializeField] GameObject _prefab;
    DropTest _dropTest;

    void Awake()
    {
        _dropTest = GetComponent<DropTest>();
    }

    public void Destruction(bool IsAlive){
        if (!IsAlive){
            _dropTest.InstantiateItem();
            DeactivateComponents();
        }
    }

    void DeactivateComponents() {
        Instantiate(_prefab,this.transform.position,Quaternion.identity);
        Destroy(this.gameObject);
    }

    public void HealthChanged(int currentHealth, int maxHealth, int damage, bool isCrit, bool isAlive){
        Destruction(isAlive);
    }
}
