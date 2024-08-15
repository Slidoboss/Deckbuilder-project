using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridCell : MonoBehaviour
{
    public Vector2 gridIndex;
    public bool cellFull = false;
    public GameObject objectInCell;
}
