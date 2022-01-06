using UnityEngine;
using UnityEngine.UI;

public class Label : MonoBehaviour{
    Text _labelText;

    public void SetLabel(string textStr){
        _labelText = GetComponentInChildren<Text>();
        _labelText.text = textStr;
        _labelText.color = Color.yellow;
    }
}
