using UnityEngine;
using UnityEngine.UI;

public class Label : MonoBehaviour
{
    Text labelText;

    public void SetLabel(string textStr){
        labelText = GetComponentInChildren<Text>();
        labelText.text = textStr;
    }
}
