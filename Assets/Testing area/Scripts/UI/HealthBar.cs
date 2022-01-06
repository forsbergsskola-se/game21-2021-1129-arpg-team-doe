using System.Security.Cryptography.X509Certificates;
using CustomLogs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour, IHealthListener{
    
    public Image healthBar;

    GameObject _parent;
    Health _health;
    Vector3 _offset = new Vector3(0, 2, 1);

    public void Awake() {
        _health = GetComponentInParent<Health>();
        _parent = GetComponentInParent<ToggleHealthBar>()?.gameObject;
        
    }

    void Update(){
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Camera.main.transform.rotation, 1f);
        transform.position = _parent.transform.position + _offset;
    }

    public void SetSliderCurrentHealth(int currentHealth){
        healthBar.fillAmount = ((float)currentHealth/(float)_health.ModifiedMaxHP);
    }

    public void HealthChanged(int currentHealth, int maxHealth, int damage, bool isCrit, bool isAlive){
        SetSliderCurrentHealth(currentHealth);
    }
}
