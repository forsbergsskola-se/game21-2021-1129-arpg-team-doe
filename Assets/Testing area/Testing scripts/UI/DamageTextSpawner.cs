using UnityEngine;
using Random = UnityEngine.Random;

public class DamageTextSpawner : MonoBehaviour, IHealthListener
{
    [SerializeField] UIDamageNr textPrefab;
    [SerializeField] float spreadRange;

    bool shouldSpawn = true;

    Vector3 RandomLocation(){
        var position = new Vector3((Random.insideUnitSphere.x * spreadRange), 1f,
            (Random.insideUnitSphere.z * spreadRange));
        return position;
    }

    public void HealthChanged(int currentHealth, int maxHealth, int damage, bool isCrit, bool isAlive){
        if (shouldSpawn){
            var damageNumber = Instantiate(textPrefab, transform.position + RandomLocation(), Quaternion.identity, transform);
            damageNumber.HealthChanged(currentHealth, maxHealth, damage,isCrit, isAlive);
        }
    }
}
