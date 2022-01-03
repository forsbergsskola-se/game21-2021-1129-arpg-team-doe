using System;
using UnityEngine;

public class ToggleLabel : MonoBehaviour
{
    [SerializeField] GameObject labelPrefab;
    [SerializeField] Vector3 offset = new Vector3(0, 2, 0);
    [SerializeField] float labelShowRange = 5f;
    GameObject _label;
    GameObject _player;
    TargetDetection _targetDetection;
    bool _labelShown;

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
            labelFaceDirection.y = 0f;
            _label.transform.rotation = Quaternion.LookRotation(labelFaceDirection);
        }
    }

    void ShowLabel(){
        Vector3 position = transform.position + offset;
        _label = Instantiate(labelPrefab, position, Quaternion.identity);
        //_label.transform.parent = gameObject.transform;
        _label.transform.SetParent(gameObject.transform);
        _label.SetActive(true);
        _label.GetComponent<Label>().SetLabel(name);
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
