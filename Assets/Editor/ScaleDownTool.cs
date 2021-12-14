using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public static class ScaleDownTool
{
    const string UNDO_STR_SCALEDOWN = "scale down";

    //This just checks if we have selected more than 0 items, if not, the menu item is grayed out
    [MenuItem("Secret Tools/Scale Down &M", isValidateFunction: true)]
    public static bool DoubleTheSizeValidate(){
        return Selection.gameObjects.Length > 0;
    }
   
    //This Records all selected items (Needed to allow saving/undoing), and then halves the vector3 for each selected gameobject
    [MenuItem("Secret Tools/Scale Down &M")]
    public static void DoubleTheSize()
    {
        foreach (GameObject _gameObject in Selection.gameObjects){
            Undo.RecordObject(_gameObject.transform,UNDO_STR_SCALEDOWN);
            _gameObject.transform.localScale = _gameObject.transform.ScaleDown();
        }
    }

    //Halves the x,y,z values and gives the vector 3 scale values
    public static Vector3 ScaleDown(this Transform _transform ){
        _transform.localScale = _transform.localScale / 2; 
        
        return _transform.localScale;
    }
}
