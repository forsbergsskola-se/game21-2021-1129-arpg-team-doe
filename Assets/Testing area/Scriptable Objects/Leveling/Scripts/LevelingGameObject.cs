using UnityEngine;

[CreateAssetMenu(fileName = "New Leveling System", menuName = "Leveling System/Leveling System")]
public class LevelingGameObject : ScriptableObject{
    [SerializeField] GameEvent _levelUp;

    [Min(0)][SerializeField] public int level = 1;
    [Min(0)][SerializeField] public int currentXP = 0;
    [Min(0)][SerializeField] public int requiredXPInt; //Used for display and starting value
    float requiredXPFloat; //Used for calculations
    [Tooltip("How much more xp you'll need each level to level up")]
    [Min(0)][SerializeField] public float xpScale;

    [Min(0)] [SerializeField] public int skillPoint;
    [Min(0)] [SerializeField] public int skillPointsPerLevel;

    [SerializeField] public GameEvent GetXPEvent;

    FMOD.Studio.EventInstance instance;
    [SerializeField] public FMODUnity.EventReference fmodEvent;
    void OnEnable(){
        requiredXPFloat = requiredXPInt;
    }

    public void ReceiveXP(int XP){
        currentXP += XP;
        CheckLevelUp();
        GetXPEvent.Invoke();
    }
    [ContextMenu("GiveXp")]
    public void GiveXp(){//used for debug
        currentXP += requiredXPInt;
        CheckLevelUp();
    }
    [ContextMenu("Check Level Up")]
    public void CheckLevelUp(){ //Every time we get xp, run this method
        while (currentXP >= requiredXPInt){
            currentXP -= requiredXPInt;
            level++;
            requiredXPFloat *= xpScale;
            requiredXPInt = (int)requiredXPFloat;
            skillPoint += skillPointsPerLevel;
            _levelUp.Invoke();
        }
    }
}
