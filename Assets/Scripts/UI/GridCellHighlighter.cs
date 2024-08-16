using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GridCellHighlighter : MonoBehaviour
{
    private SpriteRenderer _cellSprite;
    private Color _originalColor;
    private Color _highlightColor = Color.red;
    // Start is called before the first frame update
    void Start()
    {
        _cellSprite = GetComponent<SpriteRenderer>();
        _originalColor = _cellSprite.color;
    }

    void OnMouseEnter()
    {
        _cellSprite.color = _highlightColor;
    }
    void OnMouseExit()
    {
        _cellSprite.color = _originalColor;
    }
}
