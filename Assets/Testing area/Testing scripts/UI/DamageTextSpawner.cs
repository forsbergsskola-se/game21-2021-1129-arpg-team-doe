using UnityEngine;
using Random = UnityEngine.Random;

public interface ISpawner
{
    public void Spawn() { }
}

public interface ITextSpawner : ISpawner
{
    public void Spawn(int damage, bool crit);
}

public class DamageTextSpawner : MonoBehaviour, ITextSpawner, IHealthListener
{
    [SerializeField] UIDamageNr textPrefab;
    [SerializeField] float spreadRange;

    bool shouldSpawn = true;

    public void Spawn(int damage, bool crit){ }

    Vector3 RandomLocation(){
        //may change x and z between 1-3?
        var position = new Vector3((Random.insideUnitSphere.x * spreadRange), 1f,
            (Random.insideUnitSphere.z * spreadRange));
        return position;
    }

    public void HealthChanged(int currentHealth, int maxHealth, int damage, bool isCrit, bool isAlive){
        if (shouldSpawn){
            var damageNumber = Instantiate(textPrefab, transform.position + RandomLocation(), textPrefab.transform.rotation, transform);
            damageNumber.HealthChanged(currentHealth, maxHealth, damage,isCrit, isAlive);
        }
    }
}