using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width = 8;
    public int height = 4;
    public GameObject gridCellPrefab;
    public List<GameObject> gridObjects = new List<GameObject>();
    public GameObject[,] gridCells;
    private float _gridDivider = 2.0f;

    void Start()
    {
        CreateGrid();
        transform.position = new Vector3(0, 1.25f,0);
        transform.localScale = new Vector3(1.25f, 1.25f,1.25f);
    }
    void CreateGrid()
    {
        gridCells = new GameObject[width, height];
        float offset = 0.5f;
        Vector2 centerOffset = new Vector2(width / _gridDivider - offset, height / _gridDivider - offset);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 gridPosition = new Vector2(i, j);
                Vector2 spawnPosition = gridPosition - centerOffset;
                GameObject gridCell = Instantiate(gridCellPrefab, spawnPosition, Quaternion.identity);
                gridCell.transform.SetParent(transform);
                gridCell.GetComponent<GridCell>().gridIndex = gridPosition;
                gridCells[i, j] = gridCell;
            }
        }
    }
    public bool AddObjectToCell(GameObject obj, Vector2 gridPosition)
    {
        if (gridPosition.x >= 0 && gridPosition.x < width && gridPosition.y >= 0 && gridPosition.y < height)
        {
            GridCell cell = gridCells[(int)gridPosition.x,(int)gridPosition.y].GetComponent<GridCell>();

            if(cell.cellFull) return false;
            else 
            {
                GameObject newObj = Instantiate(obj, cell.GetComponent<Transform>().position, Quaternion.identity);
                newObj.transform.SetParent(transform);
                gridObjects.Add(newObj);
                cell.objectInCell = newObj;
                cell.cellFull = true;
                return true;
            }
        }
        else return false;
    }
}
