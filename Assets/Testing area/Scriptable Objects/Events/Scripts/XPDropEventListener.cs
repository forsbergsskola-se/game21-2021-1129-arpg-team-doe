using UnityEngine;

public class XPDropEventListener : MonoBehaviour{
    [SerializeField] XPDropEvent _xpDropEvent;

    void Awake() => _xpDropEvent.Register(this);
    void OnDestroy() => _xpDropEvent.Deregister(this);
    public void RaiseEvent(int xpAmount) => gameObject.GetComponent<Level>().LevelingGameObject.ReceiveXP(xpAmount);
}
