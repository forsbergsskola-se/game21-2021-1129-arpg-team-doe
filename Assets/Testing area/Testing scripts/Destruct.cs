using UnityEngine;
using UnityEngine.AI;

public interface IDestructible{
    void Destruction(bool isAlive);
}
public class Destruct : MonoBehaviour,IDestructible, IHealthListener{

    [SerializeField] Mesh _spriteIntact;
    [SerializeField] Mesh _spriteDestroyed;

    void Start(){
        GetComponent<MeshFilter>().mesh = _spriteIntact;
    }
    public void Destruction(bool IsAlive){ //Used for Debug
        if (!IsAlive){
            DeactivateComponents();
        }
    }

    void DeactivateComponents(){
        GetComponent<MeshFilter>().mesh = _spriteDestroyed;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Collider>().enabled = false;
        GetComponent<Destruct>().enabled = false;
        GetComponent<HoverInteractable>().enabled = false;
    }

    public void HealthChanged(int currentHealth, int maxHealth, int damage, bool isCrit, bool isAlive){
        Destruction(isAlive);
    }
}
