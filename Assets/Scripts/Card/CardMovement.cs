using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardMovement : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform _rectTransform;
    private Canvas _canvas;
    private RectTransform _canvasRectTransform;
    private Vector3 _originalScale;
    private int _currentState = 0;
    private Quaternion _originalRotation;
    private Vector3 _originalPosition;

    [SerializeField] private float _selectScale = 1.1f;
    [SerializeField] private Vector2 _cardPlay;
    [SerializeField] private Vector3 _playPosition;
    [SerializeField] private GameObject _glowEffect;
    [SerializeField] private GameObject _playArrow;

    [Range(0f, 1.0f)]
    [SerializeField] private float _lerpFactor;
    [SerializeField] private int _cardPlayDivider = 4;
    [SerializeField] private float _cardPlayMultiplier = 1f;
    [SerializeField] private bool _needUpdateCardPlayPosition = false;
    [SerializeField] private int _playPositionYDivider = 2;
    [SerializeField] private float _playPositionYMultiplier = 1f;
    [SerializeField] private int _playPositionXDivider = 4;
    [SerializeField] private float _playPositionXMultiplier = 1f;
    [SerializeField] private bool _needUpdatePlayPosition = false;


    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();

        if (_canvas != null)
        {
            _canvasRectTransform = _canvas.GetComponent<RectTransform>();
        }
        _originalScale = _rectTransform.localScale;
        _originalPosition = _rectTransform.localPosition;
        _originalRotation = _rectTransform.rotation;

        UpdateCardPlayPosition();
        UpdatePlayPosition();

    }


    void Update()
    {
        if (_needUpdateCardPlayPosition)
        {
            UpdateCardPlayPosition();
        }
        if (_needUpdatePlayPosition)
        {
            UpdatePlayPosition();
        }

        switch (_currentState)
        {
            case 1:
                HandleHoverState();
                break;
            case 2:
                HandleDragState();
                if (!Input.GetMouseButton(0))
                {
                    TransitionToStateZero();
                }
                break;
            case 3:
                HandlePlayState();
                if (!Input.GetMouseButton(0))
                {
                    TransitionToStateZero();
                }
                break;
        }
    }

    private void TransitionToStateZero()
    {
        _currentState = 0;
        _rectTransform.localScale = _originalScale;
        _rectTransform.localRotation = _originalRotation;
        _rectTransform.localPosition = _originalPosition;
        _glowEffect.SetActive(false);
        _playArrow.SetActive(false);
    }
    //For when the mouse is on the card.
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_currentState == 0)
        {
            _originalPosition = _rectTransform.localPosition;
            _originalRotation = _rectTransform.localRotation;
            _originalScale = _rectTransform.localScale;
            _currentState = 1;
        }
    }
    //For when the card leaves the card.
    public void OnPointerExit(PointerEventData eventData)
    {
        if (_currentState == 1)
        {
            TransitionToStateZero();
        }
    }
    //For when the Mouse is pressed.
    public void OnPointerDown(PointerEventData eventData)
    {
        if (_currentState == 1)
        {
            _currentState = 2;
        }
    }
    //For when the mouse is pressed and dragged around the screen.
    public void OnDrag(PointerEventData eventData)
    {
        if (_currentState == 2)
        {
            if (Input.mousePosition.y > _cardPlay.y)
            {
                _currentState = 3;
                _playArrow.SetActive(true);
                _rectTransform.localPosition = Vector3.Lerp(_rectTransform.position, _playPosition, _lerpFactor);

            }
        }
    }

    private void HandleHoverState()
    {
        _glowEffect.SetActive(true);
        _rectTransform.localScale = _originalScale * _selectScale;

    }

    private void HandleDragState()
    {
        //Set rotation to zero.
        _rectTransform.localRotation = Quaternion.identity;
        _rectTransform.position = Vector3.Lerp(_rectTransform.position, Input.mousePosition, _lerpFactor);
    }
    private void HandlePlayState()
    {
        _rectTransform.localPosition = _playPosition;
        _rectTransform.localRotation = Quaternion.identity;

        if (Input.mousePosition.y < _cardPlay.y)
        {
            _currentState = 2;
            _playArrow.SetActive(false);
        }
    }

    private void UpdatePlayPosition()
    {
         if (_canvasRectTransform != null && _playPositionYDivider != 0 && _playPositionXDivider != 0)
         {
            float segmentX = _playPositionXMultiplier/_playPositionXDivider;
            float segmentY = _playPositionYMultiplier/_playPositionYDivider;

            _cardPlay.x = _canvasRectTransform.rect.width * segmentX;
            _cardPlay.y = _canvasRectTransform.rect.height * segmentY;
         }
    }

    private void UpdateCardPlayPosition()
    {
        if (_cardPlayDivider != 0 && _canvasRectTransform != null)
        {
            float segment = _cardPlayMultiplier/ _cardPlayDivider;
            _cardPlay.y = _canvasRectTransform.rect.height * segment;
        }
    }

}
