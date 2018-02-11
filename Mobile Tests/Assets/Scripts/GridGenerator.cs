using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour {

	[SerializeField]
	private GameObject GridObj;
	[SerializeField]
	private Vector2Int GridSize;
	[SerializeField]
	private float CellGap;

    public Dictionary<string, bool> gridCells = new Dictionary<string, bool>();

	void Start () 
	{
        GridCreation();
	}

    public void GridCreation()
    {
        GridDestroy();

        Vector3 GridObjPos;
        Vector2 GridCenter;
        int cellNumber = 0;

        GridCenter.x = transform.position.x - (GridSize.x - 1) * (GridObj.transform.localScale.x + CellGap) / 2;
        GridCenter.y = transform.position.z - (GridSize.y - 1) * (GridObj.transform.localScale.z + CellGap) / 2;

        for (int y = 0; y < GridSize.y; y++)
        {
            for (int x = 0; x < GridSize.x; x++)
            {
                GridObjPos.x = x * (GridObj.transform.localScale.x + CellGap) + GridCenter.x;
                GridObjPos.y = transform.position.y;
                GridObjPos.z = y * (GridObj.transform.localScale.z + CellGap) + GridCenter.y;

                GameObject GridObjInst = Instantiate(GridObj, GridObjPos, GridObj.transform.rotation);
                GridObjInst.name = "Cell_" + (y+1).ToString() + "." + (x+1).ToString();
                gridCells.Add(GridObjInst.name, false);
                GridObjInst.transform.SetParent(transform);
                cellNumber++;
            }
        }
    }
    public void GridDestroy()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
		gridCells.Clear();
    }
}
