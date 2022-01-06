using UnityEngine;

namespace AnimatorChanger{
    public class AnimationController : MonoBehaviour{
        string _currentState;
        Animator _animator;

        void Start(){
            _animator = GetComponentInChildren<Animator>();
        }

        public void ChangeAnimationState(string newState){
            if (_currentState == newState) return;
            _animator.Play(newState);
            _currentState = newState;
        }
    }
}
