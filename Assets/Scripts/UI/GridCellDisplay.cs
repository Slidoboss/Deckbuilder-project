using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(GridCell))]
public class GridCellDisplay : MonoBehaviour
{
    private SpriteRenderer _cellSprite;
    private Color _originalColor;
    private Color _highlightColor = Color.cyan;
    private Color _positiveColor = Color.green;
    private Color _negativeColor = Color.red;
    private GridCell _gridCell;
    public GameObject[] backgrounds;
    private bool _setBackground = false;

    // Start is called before the first frame update
    void Start()
    {
        _cellSprite = GetComponent<SpriteRenderer>();
        _gridCell = GetComponent<GridCell>();
        _originalColor = _cellSprite.color;
    }
    void Update()
    {
        if (!_setBackground) SetBackground();
    }

    private void SetBackground()
    {
        if(_gridCell.gridIndex.x % 2 !=0)
        {
            backgrounds[0].SetActive(true);
        }
        if(_gridCell.gridIndex.y % 2 != 0)
        {
            backgrounds[1].SetActive(true);
        }
        _setBackground = true;
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
