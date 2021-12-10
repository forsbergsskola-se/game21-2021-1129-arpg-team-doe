using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public interface IHealthbar{
    void SetSliderCurrentHealth(int currentHealth);
}
public class HealthBar : MonoBehaviour, IHealthbar, IHealthListener{

    public Slider slider;

    GameObject _parent;
    Health _health;
    Quaternion _startRotation;
    Vector3 _offset = new Vector3(0, 2, 1);

    public void Start() {
        _health = GetComponentInParent<Health>();
        SetSliderMaxHealth(_health.ModifiedMaxHP);
        _startRotation = transform.rotation;
        _parent = GetComponentInParent<ToggleHealthBar>()?.gameObject;
        
    }

    void Update(){
        transform.rotation = _startRotation;
        transform.position = _parent.transform.position + _offset;
    }

    void SetSliderMaxHealth(float maxHP){
        slider.maxValue = maxHP;
        slider.value = maxHP;
    }

    public void SetSliderCurrentHealth(int currentHealth){
        slider.value = currentHealth;
    }

    public void HealthChanged(int currentHealth, int maxHealth, int damage, bool isCrit, bool isAlive){
        SetSliderCurrentHealth(currentHealth);
    }
}
