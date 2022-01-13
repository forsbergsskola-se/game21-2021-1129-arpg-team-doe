using UnityEngine;
using Random = UnityEngine.Random;

public class DropTest : MonoBehaviour{
    [SerializeField] LootTable lootTable;
    [SerializeField] GameObject cash;
    [Min(0)][SerializeField] float spreadRange;
    [Min(0)][SerializeField] int maxRollCount;
    Statistics _playerStatistics;
    int _rollCount;

    void Awake(){
        _playerStatistics = GameObject.FindWithTag("Player").GetComponent<Statistics>();
    }

    internal void InstantiateItem(){
        if (_rollCount < maxRollCount){
            _rollCount++;
            GameObject itemObject = lootTable.GetDropItem();
            if (itemObject != null){
                Instantiate(itemObject, transform.position + RandomLocation(), Quaternion.identity);
            }
            if (cash != null){
                var droppedCash = Instantiate(cash, transform.position + RandomLocation(), Quaternion.identity);
                droppedCash.name = cash.name;
            }

            if (Random.Range(0f, 1f) < _playerStatistics.LuckChance){
                InstantiateItem();
                //PlayLuckSound();
            }
        }
    }

    Vector3 RandomLocation(){
        //may change x and z between 1-3?
        var position = new Vector3((Random.insideUnitSphere.x * spreadRange), 0f,
            (Random.insideUnitSphere.z * spreadRange));
        return position;
    }
}
