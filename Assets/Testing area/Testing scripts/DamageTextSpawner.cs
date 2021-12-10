using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public interface ISpawner{
 public void Spawn(){
 }
}
public interface ITextSpawner: ISpawner{
 public void Spawn(int damage, bool crit);
 
}
public class DamageTextSpawner : MonoBehaviour, ITextSpawner{
 [SerializeField] GameObject textPrefab;
 [SerializeField] float spreadRange;
 
 bool shouldSpawn = true;
 

 public void Spawn(int damage, bool crit){
  if (shouldSpawn){
   Instantiate(textPrefab,transform.position + RandomLocation(),textPrefab.transform.rotation,transform);
  }
 }

 Vector3 RandomLocation(){ //may change x and z between 1-3?
  var position = new Vector3((Random.insideUnitSphere.x*spreadRange),1f,(Random.insideUnitSphere.z*spreadRange));
  return position;
 }
}
