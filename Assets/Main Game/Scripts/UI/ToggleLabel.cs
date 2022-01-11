using System;
using MoreColors;
using UnityEngine;

[RequireComponent(typeof(Label))]
[RequireComponent(typeof(TargetDetection))]
public class ToggleLabel : MonoBehaviour{
    [SerializeField] GameObject labelCanvas;
    [SerializeField] Vector3 offset = new Vector3(0, 2, 0);
    [SerializeField] float labelShowRange = 5f;
    GameObject _label;
    GameObject _player;
    TargetDetection _targetDetection;
    bool _labelShown;
    String _shownName;

    void Start(){
        _player = GameObject.FindWithTag("Player");
        _targetDetection = GetComponent<TargetDetection>();
    }

    void Update(){
        if (IsInRange() && _label == null){
            ShowLabel();
            _labelShown = true;
            return;
        }
        if(!IsInRange()){
            TurnOffLabel();
            _labelShown = false;
        }

        if (_labelShown){
            RotateLabel();
        }
    }

    void RotateLabel(){
        if (Camera.main is not null){
            Vector3 labelFaceDirection = _label.transform.position - Camera.main.transform.position;
            _label.transform.rotation = Quaternion.LookRotation(labelFaceDirection);
        }
    }

    void ShowLabel(){
        Vector3 position = transform.position + offset;
        _label = Instantiate(labelCanvas, position, Quaternion.identity);
        _label.transform.SetParent(gameObject.transform);
        _label.SetActive(true);
        
        if (GetComponent<Currency>() != null)
        {
            _shownName = GetComponent<Currency>()?.currencyData.name;
            _label.GetComponent<Label>().SetLabel(GetComponent<Currency>().amount + " " +_shownName, Color.yellow);
        }
        else{
            var item = GetComponent<InventoryItem>()?.itemObject;
            _shownName = item.name;
            if (item.isEasterEgg){
                _label.GetComponent<Label>().SetLabel(_shownName, Color.magenta);
            }
            else if (item is ConsumableObject){
                _label.GetComponent<Label>().SetLabel(_shownName, Colors.lightGreen);
            }
            else{
                _label.GetComponent<Label>().SetLabel(_shownName, Color.white);
            }
        }
    }
    
    void TurnOffLabel(){
        if (_label != null){
            Destroy(_label);
        }
    }

    bool IsInRange(){
        var distance = _targetDetection.DistanceToTarget(transform.position, _player.transform);
        return distance < labelShowRange;
    }
}
