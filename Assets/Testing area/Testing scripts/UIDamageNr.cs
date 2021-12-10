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

    Animator _animator;

    string dmgText;
    bool activeTimer;
    bool takingDamage;

    const string FLOATING_POINT = "FloatingPoint";

    void Start(){
        _animator = gameObject.GetComponent<Animator>();
    }

    public void DisplayDmg(int damage, bool isCrit){
        dmgText = Convert.ToString(damage);
        //Debug.Log("im displaying");
        FontChange(isCrit);
        Timer();
        SetText();
    }

    async void Timer(){
        //Converting milliseconds to seconds.
        await Task.Delay((int)duration*1000);

       // ClearText(); //Temporary
       if (this != null){
           Destroy(gameObject);
       }
    }

    void SetText(){
        _textMeshProUGUI.text = dmgText;
    }

    void ClearText(){
        _textMeshProUGUI.text = "";
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
