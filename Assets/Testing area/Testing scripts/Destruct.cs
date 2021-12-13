using UnityEngine;
using UnityEngine.AI;

public interface IDestructible{
    void Destruction(bool isAlive);
}
public class Destruct : MonoBehaviour,IDestructible, IHealthListener{
    
    [SerializeField] GameObject _prefab;

    public void Destruction(bool IsAlive){ 
        if (!IsAlive){
            DeactivateComponents();
        }
    }

    void DeactivateComponents(){

        Instantiate(_prefab,this.transform.position,Quaternion.identity);
        Destroy(this.gameObject);
    }

    public void HealthChanged(int currentHealth, int maxHealth, int damage, bool isCrit, bool isAlive){
        Destruction(isAlive);
    }
}
