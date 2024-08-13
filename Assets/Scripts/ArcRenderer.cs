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
    private List<GameObject> _dotPool = new List<GameObject>();
    private GameObject _arrowInstance;
    private Vector3 _arrowDirection;


    void Start()
    {
        _arrowInstance = Instantiate(arrowPrefab,transform);
        _arrowInstance.transform.localPosition = Vector3.zero;
        InitializeDotPool(poolSize);
    }


    void Update()
    {
        
    }
    private void InitializeDotPool(int poolSize)
    {
        for (int i = 0; i< poolSize; i++)
        {
            GameObject dot = Instantiate(dotPrefab,Vector3.zero,Quaternion.identity,transform);
            dot.SetActive(false);
            _dotPool.Add(dot);
        }
    }
}
