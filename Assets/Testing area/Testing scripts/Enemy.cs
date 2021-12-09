using UnityEngine;

public class Enemy : MonoBehaviour{

   Statistics _statistics;
   CapsuleCollider _capsuleCollider;

   void Start(){
      _statistics = GetComponent<Statistics>();
      _capsuleCollider = GetComponent<CapsuleCollider>();
   }

   void Update(){
      if (!_statistics.IsAlive) { Die(); } // debug
   }

   void Die(){
      // call death event (animations, sounds etc)
      Debug.Log(this.name + " Dead");
      _capsuleCollider.enabled = false;
   }
}
