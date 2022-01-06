using UnityEngine;

public class ReceiveXPEventListener : MonoBehaviour
{
    [SerializeField] ReceiveXPEvent _receiveXpEvent;

    void Awake() => _receiveXpEvent.Register(this);
    void OnDestroy() => _receiveXpEvent.Deregister(this);
    public void RaiseEvent() => gameObject.GetComponent<XPBar>().SetXPBar();
}
