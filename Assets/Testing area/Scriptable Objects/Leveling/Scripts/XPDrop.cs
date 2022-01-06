using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPDrop : MonoBehaviour
{
   [Tooltip("The amount of XP granted to player")]
   [SerializeField] public XPDropEvent _xpDropEvent;
   [SerializeField] public int xpAmount;
}
