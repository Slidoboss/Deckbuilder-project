using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardMovement : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform _rectTransform;
    private Canvas _canvas;
    private Vector2 _originalLocalPointerPosition;
    private Vector3 _originalPanelLocalPosition;
    private Vector3 _originalScale;
    private int _currentState = 0;
    private Quaternion _originalRotation;
    private Vector3 _originalPosition;

    [SerializeField] private float _selectScale = 1.1f;
    [SerializeField] private Vector2 _cardPlay;
    [SerializeField] private Vector3 _playPosition;
    [SerializeField] private GameObject _glowEffect;
    [SerializeField] private GameObject _playArrow;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        _originalScale = _rectTransform.localScale;
        _originalPosition = _rectTransform.localPosition;
        _originalRotation = _rectTransform.rotation;
    }

    void Update()
    {
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
        _rectTransform.rotation = _originalRotation;
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
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out _originalLocalPointerPosition);
            _originalPanelLocalPosition = _rectTransform.localPosition;

        }
    }
    //For when the mouse is pressed and dragged around the screen.
    public void OnDrag(PointerEventData eventData)
    {
        if (_currentState == 2)
        {
            if (_currentState == 2)
            {
                Vector2 localPointerPosition;
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out localPointerPosition))
                {
                    _rectTransform.position = Input.mousePosition;

                    if (_rectTransform.localPosition.y > _cardPlay.y)
                    {
                        _currentState = 3;
                        _playArrow.SetActive(true);
                        _rectTransform.localPosition = _playPosition;
                    }
                }
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
    }
    private void HandlePlayState()
    {
        _rectTransform.localPosition = _playPosition;
        _rectTransform.localRotation = Quaternion.identity;

        if(Input.mousePosition.y < _playPosition.y)
        {
            _currentState = 2;
            _playArrow.SetActive(false);
        }
    }

}
