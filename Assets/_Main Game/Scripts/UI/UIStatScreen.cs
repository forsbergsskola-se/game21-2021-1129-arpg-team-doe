using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIStatScreen : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
    [SerializeField] Button applyButton;
    [SerializeField] GameObject SkillPointTextObject;
    [SerializeField] GameObject LevelTextObject;
    [SerializeField] LevelingGameObject _playerLevel;
    [SerializeField] UIStats _uiStats;

    int availableSkillPoints;

    TextMeshProUGUI SkillPointText;
    TextMeshProUGUI LevelText;
    InventoryController _inventoryController;

    void Awake()
    {
        SkillPointText = SkillPointTextObject.GetComponent<TextMeshProUGUI>();
        _inventoryController = FindObjectOfType<InventoryController>();
        LevelText = LevelTextObject.GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        SetAvailableSkillPoints();
        SetSkillPointText();
        SetLevelText();
    }

    void Update(){
        if (availableSkillPoints != _playerLevel.skillPoint){
            SetAvailableSkillPoints();
            SetSkillPointText();
        }
    }

    public void SetLevelText(){
        LevelText.text = "Level: " + _playerLevel.level.ToString();
    }

    [ContextMenu("OnDisable")]
    void OnDisable(){
        applyButton.onClick.Invoke();
    }

    public void SetAvailableSkillPoints(){
        availableSkillPoints = _playerLevel.skillPoint;
    }

    public void SetSkillPointText(){
        SkillPointText.text = ("Skill Points: " + availableSkillPoints.ToString()); 
    }
    
    public void OnPointerEnter(PointerEventData eventData){
        _inventoryController.clickOnUI = true;
    }
    
    public void OnPointerExit(PointerEventData eventData){
        _inventoryController.clickOnUI = false;
    }
}