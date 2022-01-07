using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStatScreen : MonoBehaviour{
    [SerializeField] Button applyButton;
    [SerializeField] GameObject SkillPointTextObject;
    [SerializeField] LevelingGameObject _playerLevel;
    
    int availableSkillPoints;

    string SkillPointText;

    void Awake()
    {
        SkillPointText = SkillPointTextObject.GetComponent<TextMeshProUGUI>().text;
    }

    void Start()
    {
        SetAvailableSkillPoints();
        SetSkillPointText();
    }

    void Update()
    {
        
        if (availableSkillPoints != _playerLevel.skillPoint)
        {
            
            SetAvailableSkillPoints();
            SetSkillPointText();
        }
    }

    [ContextMenu("OnDisable")]
    void OnDisable(){
        applyButton.onClick.Invoke();
    }

    public void SetAvailableSkillPoints()
    {
        availableSkillPoints = _playerLevel.skillPoint;
    }

    public void SetSkillPointText()
    {
        SkillPointText = ("Skill Points: " + availableSkillPoints.ToString()); //Skill points;
    }
}
