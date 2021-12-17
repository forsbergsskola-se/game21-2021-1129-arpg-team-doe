using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour, IHealthbar
{
    public Slider slider;
    public Health health;
    bool _isSliderShown;

    void Start(){
        //slider.maxValue = health.ModifiedMaxHP;
    }

    void Update(){
        if (_isSliderShown){
            SetSliderCurrentHealth(health.CurrentHP);
            return;
        }
        if (!_isSliderShown){
            _isSliderShown = true;
            SetSliderMaxHealth(health.ModifiedMaxHP);
        }
    }

    void SetSliderMaxHealth(int maxHp){
        slider.maxValue = maxHp;
        slider.value = maxHp;
    }

    public void SetSliderCurrentHealth(int currentHealth){
        slider.value = currentHealth;
    }
}