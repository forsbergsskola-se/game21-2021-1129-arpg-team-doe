using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStatScreen : MonoBehaviour{
    [SerializeField] Button applyButton;
    [SerializeField] GameObject SkillPointTextObject;
    [SerializeField] LevelingGameObject _playerLevel;
    [SerializeField] UIStats _uiStats;

    int availableSkillPoints;

    TextMeshProUGUI SkillPointText;

    void Awake()
    {
        SkillPointText = SkillPointTextObject.GetComponent<TextMeshProUGUI>();
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
        SkillPointText.text = ("Skill Points: " + availableSkillPoints.ToString()); 
    }
}