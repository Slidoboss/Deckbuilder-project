using System;
using System.Collections.Generic;
using UnityEngine;

public class ArcRenderer : MonoBehaviour
{
    public GameObject arrowPrefab;
    public GameObject dotPrefab;
    public int poolSize = 50;
    public float spacing = 50;
    public float arrowAngleAdjustment = 0;
    public int dotToSkip = 1;
    public float baseScreenWidth = 1920f;
    [SerializeField] private float _spacingScale;
    private List<GameObject> _dotPool = new List<GameObject>();
    private GameObject _arrowInstance;
    private Vector3 _arrowDirection;


    void Start()
    {
        //"transform" is getting the transform component this script is attached to.
        _arrowInstance = Instantiate(arrowPrefab, transform);
        _arrowInstance.transform.localPosition = Vector3.zero;
        InitializeDotPool(poolSize);

        _spacingScale = Screen.width / baseScreenWidth;//Scales the dot spacing based on the current width of the screen.
    }
    void OnEnable()
    {
        _spacingScale = Screen.width / baseScreenWidth;
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0f;

        Vector3 startPos = transform.position;
        Vector3 midPoint = CalculateMidPoint(startPos, mousePos);

        UpdateArc(startPos, midPoint, mousePos);
        PositionAndRotateArrow(mousePos);
    }

    private void InitializeDotPool(int poolSize)
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject dot = Instantiate(dotPrefab, Vector3.zero, Quaternion.identity, transform);
            dot.SetActive(false);
            _dotPool.Add(dot);
        }
    }

    private void UpdateArc(Vector3 startPos, Vector3 midPoint, Vector3 mousePos)
    {
        int numDots = Mathf.CeilToInt(Vector3.Distance(startPos, mousePos) / (spacing * _spacingScale));

        for (int i = 0; i < numDots && i < _dotPool.Count; i++)
        {
            float t = i / (float)numDots; //note 0<=t<=1
            t = Mathf.Clamp(t, 0f, 1f); //insurance

            Vector3 position = QuadraticBezierPoint(startPos, midPoint, mousePos, t);

            if (i != numDots - dotToSkip)
            {
                _dotPool[i].transform.position = position;
                _dotPool[i].SetActive(true);
            }
            else if (i == numDots - (dotToSkip + 1) && i - dotToSkip + 1 >= 0)
            {
                _arrowDirection = _dotPool[i].transform.position;
            }
        }

        for (int i = numDots - dotToSkip; i < _dotPool.Count; i++)
        {
            if (i > 0)
            {
                _dotPool[i].SetActive(false);
            }
        }
    }

    private Vector3 QuadraticBezierPoint(Vector3 startPos, Vector3 midPoint, Vector3 mousePos, float t)
    {
        //This function basically does this ==> B(t) = (1-t)^2 * P0 + 2(1-t)t * P1 + t^2 * P2. 
        //P1,P2 & P3 are vector locations. 
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 point = uu * startPos;
        point += 2 * u * t * midPoint;
        point += tt * mousePos;
        return point;
    }

    private Vector3 CalculateMidPoint(Vector3 startPos, Vector3 mousePos)
    {
        Vector3 midPoint = (startPos + mousePos) / 2;
        float arcHeight = Vector3.Distance(startPos, mousePos) / 3f;
        midPoint.y += arcHeight;
        return midPoint;
    }

    private void PositionAndRotateArrow(Vector3 position)
    {
        _arrowInstance.transform.position = position;
        Vector3 direction = _arrowDirection - position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle += arrowAngleAdjustment;
        _arrowInstance.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }
}
