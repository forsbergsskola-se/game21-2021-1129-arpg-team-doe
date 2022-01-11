using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public interface IDamageNumbers{
    public void DisplayDmg(int damage, bool isCrit);
}
public class UIDamageNr : MonoBehaviour, IDamageNumbers, IHealthListener{

    [SerializeField] float duration;
    [SerializeField] TextMeshProUGUI _textMeshProUGUI;
    

    string dmgText;
    bool activeTimer;
    bool takingDamage;

    void Update(){
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Camera.main.transform.rotation, 10f);
    }

    public void DisplayDmg(int damage, bool isCrit){
        dmgText = Convert.ToString(damage);
        FontChange(isCrit);
        Timer();
        SetText();
    }

    async void Timer(){
        //Converting milliseconds to seconds.
        await Task.Delay((int)duration*1000);
        
       if (this != null){
           Destroy(gameObject);
       }
    }

    void SetText(){
        _textMeshProUGUI.text = dmgText;
    }
    

    void FontChange(bool isCrit){
        if (isCrit){
            _textMeshProUGUI.color = Color.yellow;
        }
        else{
            _textMeshProUGUI.color = Color.white;
        }
    }

    public void HealthChanged(int currentHealth, int maxHealth, int damage, bool isCrit, bool isAlive){
        DisplayDmg(damage, isCrit);
    }
}
