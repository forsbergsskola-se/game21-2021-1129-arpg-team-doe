using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStats : MonoBehaviour
{
    
    [SerializeField] Statistics _playerStatistics;
    [SerializeField] TextMeshProUGUI _attributeText;
    [SerializeField] TextMeshProUGUI _valueText;
    [SerializeField] LevelingGameObject _playerLevel;
    [SerializeField] Button _increaseButton;
    [SerializeField] Button _decreaseButton;
    int availableSkillPoints;
    int usedSkillPoints;
    float currentToughness;
    float currentStrength;
    float currentDexterity;
    float currentKnowledge;
    float currentReflex;
    float currentLuck;
    bool hasStarted = false;

    public bool NeedToApplySkills{
        get
        {
            if (usedSkillPoints > 0){
                return true;
            }

            return false;
        }
    }


    //float attribute;
    void Awake(){
       _playerStatistics = GameObject.FindWithTag("Player").GetComponent<Statistics>();
       
       
       
    }

    void OnEnable(){
        AttributeAssingment();
        UpdateUIStats();
        hasStarted = true;
    }

    void Start(){
        
        AttributeAssingment();
        UpdateUIStats();
    }

    public void AttributeAssingment(){
        availableSkillPoints = _playerLevel.skillPoint;
        currentToughness = _playerStatistics.Toughness;
        currentStrength = _playerStatistics.Strength;
        currentDexterity = _playerStatistics.Dexterity;
        currentKnowledge = _playerStatistics.Knowledge;
        currentReflex = _playerStatistics.Reflex;
        currentLuck = _playerStatistics.Luck;
    }
 
    public void ApplySkillPoints(){
        AttributeAssingment();
        UpdateUIStats();
        usedSkillPoints = 0;
        _decreaseButton.gameObject.SetActive(false);
    }

    void Update(){
        //Var original value
        //when we press increase the minus button enables for x amount of points we put in
        //when we are out of skillPoints we disable the increase
        //add apply button to add all skills to stats
        //when we press apply it updates stats
        
    }

    public void ChangeSkillPoint(int amount){
        availableSkillPoints += amount;
        _playerLevel.skillPoint += amount;
        usedSkillPoints-= amount;
        UpdateUIStats();
    }
    


    //Call After applying skill point in button script
    [ContextMenu("UpdateUIStats")]
    public void UpdateUIStats(){ //"This is bad, but it works" - Creator of ze code

        if (_playerLevel.skillPoint <= 0){
            _increaseButton.gameObject.SetActive(false);
            
        }
          if (_playerLevel.skillPoint > 0 || hasStarted == false){
            _increaseButton.gameObject.SetActive(true);
            if (_attributeText.text == nameof(_playerStatistics.Toughness)){
                _valueText.text = ((int)_playerStatistics.Toughness).ToString();
                if (currentToughness < _playerStatistics.Toughness){
                    _decreaseButton.gameObject.SetActive(true);
                }
                else{
                    _decreaseButton.gameObject.SetActive(false);
                }
            }
            if (_attributeText.text == nameof(_playerStatistics.Strength)){
                _valueText.text = ((int)_playerStatistics.Strength).ToString();
                if (currentStrength < _playerStatistics.Strength){
                    _decreaseButton.gameObject.SetActive(true);
                }
                else{
                    _decreaseButton.gameObject.SetActive(false);
                }
            }
            if (_attributeText.text == nameof(_playerStatistics.Dexterity)){
                _valueText.text = ((int)_playerStatistics.Dexterity).ToString();
                if (currentDexterity < _playerStatistics.Dexterity){
                    _decreaseButton.gameObject.SetActive(true);
                }
                else{
                    _decreaseButton.gameObject.SetActive(false);
                }
            }
            if (_attributeText.text == nameof(_playerStatistics.Knowledge)){
                _valueText.text = ((int)_playerStatistics.Knowledge).ToString();
                if (currentKnowledge < _playerStatistics.Knowledge){
                    _decreaseButton.gameObject.SetActive(true);
                }
                else{
                    _decreaseButton.gameObject.SetActive(false);
                }
            }
            if (_attributeText.text == nameof(_playerStatistics.Reflex)){
                _valueText.text =((int)_playerStatistics.Reflex).ToString();
                if (currentReflex < _playerStatistics.Reflex){
                    _decreaseButton.gameObject.SetActive(true);
                }
                else{
                    _decreaseButton.gameObject.SetActive(false);
                }
            }
            if (_attributeText.text == nameof(_playerStatistics.Luck)){
                _valueText.text = ((int)_playerStatistics.Luck).ToString();
                if (currentLuck < _playerStatistics.Luck){
                    _decreaseButton.gameObject.SetActive(true);
                }
                else{
                    _decreaseButton.gameObject.SetActive(false);
                }
            }
        }
    }
}
