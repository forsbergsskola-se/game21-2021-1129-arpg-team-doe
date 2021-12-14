using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public static class Snapper{
   const string UNDO_STR_SNAP = "snap objects";

   //This just checks if we have selected more than 0 items, if not, the menu item is grayed out
   [MenuItem("Secret Tools/Snap Selected Objects %&Z", isValidateFunction: true)]
   public static bool SnapItemsValidate(){
      return Selection.gameObjects.Length > 0;
   }
   
   //This Records all selected items (Needed to allow saving/undoing), and then rounds the transforms for each selected items
   [MenuItem("Secret Tools/Snap Selected Objects %&Z")]
   public static void SnapItems()
   {
      foreach (GameObject _gameObject in Selection.gameObjects){
         Undo.RecordObject(_gameObject.transform,UNDO_STR_SNAP);
         _gameObject.transform.position = _gameObject.transform.position.Round();
      }
   }

   //Rounds the  x,y,z values and gives the rounded vector 3
   public static Vector3 Round(this Vector3 vector3 ){
      vector3.x = MathF.Round(vector3.x);
      vector3.y = MathF.Round(vector3.y);
      vector3.z = MathF.Round(vector3.z);
      return vector3;
   }
}
