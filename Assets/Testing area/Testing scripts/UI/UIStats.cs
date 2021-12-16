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


    //float attribute;

    void Start(){
        _playerStatistics = GameObject.FindWithTag("Player").GetComponent<Statistics>();
        UpdateUIStats();
       
    }

    
    //Call After applying skill point in button script
    public void UpdateUIStats(){ //"This is bad, but it works" - Creator of ze code
        if (_attributeText.text == nameof(_playerStatistics.Toughness)){
            _valueText.text = ((int)_playerStatistics.Toughness).ToString();
        }
        if (_attributeText.text == nameof(_playerStatistics.Strength)){
            _valueText.text = ((int)_playerStatistics.Strength).ToString();
        }
        if (_attributeText.text == nameof(_playerStatistics.Dexterity)){
            _valueText.text = ((int)_playerStatistics.Dexterity).ToString();
        }
        if (_attributeText.text == nameof(_playerStatistics.Knowledge)){
            _valueText.text = ((int)_playerStatistics.Knowledge).ToString();
        }
        if (_attributeText.text == nameof(_playerStatistics.Reflex)){
            _valueText.text =((int)_playerStatistics.Reflex).ToString();
        }
        if (_attributeText.text == nameof(_playerStatistics.Luck)){
            _valueText.text = ((int)_playerStatistics.Luck).ToString();
        }
    }
}
