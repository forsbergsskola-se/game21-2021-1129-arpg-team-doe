using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public interface IDamageNumbers{
    public void DisplayDmg(int damage, bool isCrit);
}
public class UIDamageNr : MonoBehaviour{

    [SerializeField] float duration;
    [SerializeField] TextMeshProUGUI _textMeshProUGUI;

    Animator _animator;
    
    string dmgText;
    bool activeTimer;
    bool takingDamage;
    
    const string FLOATING_POINT = "FloatingPoint";
    
    

    void Start(){
        _animator = gameObject.GetComponent<Animator>();
        _animator.enabled = false;
    }
    
    void Update(){ //Debug
        DealDmg();
    }

    public void DealDmg(){ //used for debug
        if (Input.GetKeyDown(KeyCode.E)){
            DisplayDmg(10, true);
        }
    }
    void DisplayDmg(int damage, bool isCrit){ //used for Debug
        dmgText = Convert.ToString(damage);
        FontChange(isCrit);
        Timer();
        SetAndPlayText();
    } 

    public void Display(){
      //  activeTimer = true;
        //duration = 1;
    }
    
    async void Timer(){
        //Converting milliseconds to seconds.
        await Task.Delay((int)duration*1000);
        
       // ClearText(); //Temporary
        Destroy(gameObject);
    }

    void SetAndPlayText(){
        _textMeshProUGUI.text = dmgText;
        _animator.enabled = true;
        _animator.Play(FLOATING_POINT);
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
}
