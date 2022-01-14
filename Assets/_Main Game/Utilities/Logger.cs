using UnityEngine;
using Object = UnityEngine.Object;
using GameObject = UnityEngine.GameObject;
namespace CustomLogs{
   public static class Logger{
      public static string critMessage = ""; 
      public static void Log(this Object myObject, object message){
         Debug.Log($"[<color=lightblue>{myObject.name}</color>]: {message}");
      }
      public static void LogHealth(this Object myObject, int currentHealth){
         Debug.Log($"[<color=lightblue>{myObject.name}</color>] I have [<color=green>{currentHealth}</color>] health!");
      }
      public static void LogPosition(this Object myObject,GameObject myGameObject){
         Debug.Log($"[<color=lightblue>{myObject.name}</color>] My position is: [<color=gray>{myGameObject.transform.position}</color>]");
      }
      public static void LogTakeDamage(this Object myObject,int damage){
         Debug.Log($"[<color=lightblue>{myObject.name}</color>] I took: [<color=red>{damage}</color>] Damage!");
      }
      public static void LogTakeDamage(this Object myObject,int damage,int currentHealth){
         Debug.Log($"[<color=lightblue>{myObject.name}</color>] I took: [<color=red>{damage}</color>] Damage! I now have [<color=green>{currentHealth}</color>] Health");
      }
      public static void LogDealDamage(this Object myObject,int damage, GameObject target){
         Debug.Log($"[<color=lightblue>{myObject.name}</color>] I am dealing: [<color=red>{damage}</color>] Damage to [<color=orange>{target.name}</color>]");
      }
      public static void LogDealDamage(this Object myObject,int damage, GameObject target, int targetCurrentHealth){
         Debug.Log($"[<color=lightblue>{myObject.name}</color>] I am dealing: [<color=red>{damage}</color>] Damage to [<color=orange>{target.name}</color>] and it now has [<color=green>{targetCurrentHealth}</color>] Health");
      }
      public static void LogDealDamage(this Object myObject,int damage, bool crit, GameObject target, int targetCurrentHealth){
         if (crit){
             critMessage = "Critical hit!";
         }
         if (crit == false){
             critMessage = "Non-Crit";
         }
         Debug.Log($"[<color=lightblue>{myObject.name}</color>] I am dealing: [<color=red>{damage}</color>] Damage [<color=yellow>{critMessage}</color>] to [<color=orange>{target.name}</color>] and it now has [<color=green>{targetCurrentHealth}</color>] Health");
      }
   }
}

