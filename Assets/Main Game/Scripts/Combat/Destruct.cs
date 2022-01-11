using UnityEngine;

public class Destruct : MonoBehaviour, IHealthListener{
    [SerializeField] GameObject _prefab;
    DropTest[] _dropTest;
    AnimationController _animationController;
    const string IDLE = "Idle";

    void Awake(){
        _dropTest = GetComponents<DropTest>();
        _animationController = GameObject.FindWithTag("Player").GetComponent<AnimationController>();
    }

    void Destruction(bool isAlive){
        if (!isAlive){
            _animationController.ChangeAnimationState(IDLE);
            foreach (var dropTest in _dropTest){
                dropTest.InstantiateItem();
            }
            DeactivateComponents();
        }
    }

    void DeactivateComponents(){
        _prefab.SetActive(true);
        this.GetComponent<MeshRenderer>().enabled = false;
        // Instantiate(_prefab,transform.position,Quaternion.identity);
        // Destroy(gameObject);
    }

    public void HealthChanged(int currentHealth, int maxHealth, int damage, bool isCrit, bool isAlive){
        Destruction(isAlive);
    }
}
