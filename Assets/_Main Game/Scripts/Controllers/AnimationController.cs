using UnityEngine;

public class AnimationController : MonoBehaviour{
    Animator _animator;
    string _currentState;

    void Start(){
        _animator = GetComponentInChildren<Animator>();
    }

    public void ChangeAnimationState(string newState){
        if (_currentState == newState) return;
        _animator.Play(newState);
        _currentState = newState;
    }
}