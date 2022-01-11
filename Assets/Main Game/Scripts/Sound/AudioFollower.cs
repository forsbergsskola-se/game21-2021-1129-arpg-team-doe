using UnityEngine;

public class AudioFollower : MonoBehaviour{
    [SerializeField] Vector3 offset;
    Transform player;

    void Start(){
        player = FindObjectOfType<PlayerController>().transform;
    }

    void Update(){
        var transform1 = transform;
        transform1.position = player.position + offset;
        transform.position = transform1.position;
    }
}
