using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Currency : MonoBehaviour{
     [SerializeField] internal CurrencyData currencyData;
    internal int amount;

    void Start(){
        GenerateValue();
    }

    void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(PickUpTimer(other));
            
        }
    }
    void GenerateValue(){
        amount = Random.Range(currencyData.minAmount, currencyData.maxAmount);
    }

    IEnumerator PickUpTimer(Collider other)
    {
        yield return new WaitForSeconds(1);
        other.gameObject.GetComponent<CurrencyHolder>()._currencyHolderDataSo.AddCurrency(amount);
        Destroy(gameObject);
    }
}
