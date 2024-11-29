
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Helpers
{
 public static Camera _camera;
 /// <summary>
 /// This reduces the cost for calling camera as it is cached.
 /// </summary>
 /// <returns></returns>
   public static Camera Camera
   {
      get
      {
         if (_camera == null) _camera = Camera.main;
         return _camera;
      }
   }

   static readonly Dictionary<float, WaitForSeconds> waitForSecondsDictionary = new Dictionary<float, WaitForSeconds>();
/// <summary>
/// This reduces garbage collection by reusing Waitforseconds.
/// </summary>
/// <param name="time"></param>
/// <returns></returns>
   public static WaitForSeconds GetWait(float time)
   {
      if (waitForSecondsDictionary.TryGetValue(time, out var waitForSeconds)) return waitForSeconds;

      waitForSecondsDictionary[time] = new WaitForSeconds(time);
      return waitForSecondsDictionary[time];
   }

   static PointerEventData _eventDataCurrentPosition;
   static List<RaycastResult> _results;
/// <summary>
/// This checks if your cursor is over UI
/// </summary>
/// <returns></returns>
   public static bool IsOverUi()
   {
      _eventDataCurrentPosition = new PointerEventData(EventSystem.current)
      {
         position = Input.mousePosition
      };
      _results = new List<RaycastResult>();
      EventSystem.current.RaycastAll(_eventDataCurrentPosition,_results);
      return _results.Count > 0;
   }
/// <summary>
/// This will tell you exactly where the item should be placed in order to be under the target
/// </summary>
/// <param name="element"></param>
/// <returns></returns>
   public static Vector2 GetWorldPositionOfCanvasElement(RectTransform element)
   {
      RectTransformUtility.ScreenPointToWorldPointInRectangle(element, element.position, Camera, out var result);
      return result;
   }

   /// <summary>
   /// Deletes all child objects with a transform of this Object.
   /// </summary>
   /// <param name="transform"></param>
   public static void DeleteChildren(this Transform transform)
   {
      foreach (Transform child in transform)
      {
         Object.Destroy(child.gameObject);
      }
   }


}
