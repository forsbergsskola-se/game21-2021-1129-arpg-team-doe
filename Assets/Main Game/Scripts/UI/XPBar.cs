using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour{
    [SerializeField] LevelingGameObject playerXP;
    public Image image;
    public float XPValue;
    
    void Awake(){
        SetXPBar();
    }
    
    public void SetXPBar(){ 
        XPValue = ((float)playerXP.currentXP/(float)playerXP.requiredXPInt);
        image.fillAmount = XPValue;
    }
}
