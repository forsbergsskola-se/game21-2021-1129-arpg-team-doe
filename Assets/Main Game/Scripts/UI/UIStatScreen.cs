using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIStatScreen : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
    [SerializeField] Button applyButton;
    [SerializeField] GameObject SkillPointTextObject;
    [SerializeField] LevelingGameObject _playerLevel;
    [SerializeField] UIStats _uiStats;

    int availableSkillPoints;

    TextMeshProUGUI SkillPointText;
    InventoryController _inventoryController;

    void Awake()
    {
        SkillPointText = SkillPointTextObject.GetComponent<TextMeshProUGUI>();
        _inventoryController = FindObjectOfType<InventoryController>();
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
    
    public void OnPointerEnter(PointerEventData eventData){
        _inventoryController.mouseOnUI = true;
    }
    
    public void OnPointerExit(PointerEventData eventData){
        _inventoryController.mouseOnUI = false;
    }
}