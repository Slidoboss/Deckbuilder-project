using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GridCellDisplay : MonoBehaviour
{
    private SpriteRenderer _cellSprite;
    private Color _originalColor;
    private Color _highlightColor = Color.cyan;
    private Color _positiveColor = Color.green;
    private Color _negativeColor = Color.red;
    private GridCell _gridCell;

    // Start is called before the first frame update
    void Start()
    {
        _cellSprite = GetComponent<SpriteRenderer>();
        _gridCell = GetComponent<GridCell>();
        _originalColor = _cellSprite.color;
    }

    void OnMouseEnter()
    {
        if (!GameManager.Instance.isPlayingCard)
        {
            _cellSprite.color = _highlightColor;
        }
        else if (_gridCell.cellFull || _gridCell.gridIndex.x > 1)
        {
            _cellSprite.color = _negativeColor;
        }
        else
        {
            _cellSprite.color = _positiveColor;
        }
    }
    void OnMouseExit()
    {
        _cellSprite.color = _originalColor;
    }
}
