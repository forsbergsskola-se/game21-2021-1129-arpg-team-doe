using UnityEngine;

[CreateAssetMenu(fileName = "New Grant XP", menuName = "Leveling System/Grant XP")]

public class GrantXPGameObject : ScriptableObject{
    [SerializeField] LevelingGameObject levelingGameObject;

    public void GrantXP(int XPAmount){
        levelingGameObject.ReceiveXP(XPAmount);
    }
}
