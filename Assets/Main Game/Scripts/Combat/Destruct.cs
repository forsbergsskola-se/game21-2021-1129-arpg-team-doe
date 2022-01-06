using UnityEngine;

public class Destruct : MonoBehaviour, IHealthListener{
    [SerializeField] GameObject _prefab;
    DropTest _dropTest;
    AnimationController _animationController;
    const string IDLE = "Idle";

    void Awake(){
        _dropTest = GetComponent<DropTest>();
        _animationController = GameObject.FindWithTag("Player").GetComponent<AnimationController>();
    }

    void Destruction(bool isAlive){
        if (!isAlive){
            _animationController.ChangeAnimationState(IDLE);
            _dropTest.InstantiateItem();
            DeactivateComponents();
        }
    }

    void DeactivateComponents() {
        Instantiate(_prefab,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }

    public void HealthChanged(int currentHealth, int maxHealth, int damage, bool isCrit, bool isAlive){
        Destruction(isAlive);
    }
}
