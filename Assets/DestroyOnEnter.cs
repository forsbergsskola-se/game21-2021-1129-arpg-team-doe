using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnEnter : MonoBehaviour
{
    IEnumerator playOneShot() {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }


    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            StartCoroutine(playOneShot());

        }
    }


}
