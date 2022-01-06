using UnityEngine;

public class Destruct : MonoBehaviour, IHealthListener{
    
    [SerializeField] GameObject _prefab;
    DropTest _dropTest;

    void Awake(){
        _dropTest = GetComponent<DropTest>();
    }

    void Destruction(bool isAlive){
        if (!isAlive){
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
