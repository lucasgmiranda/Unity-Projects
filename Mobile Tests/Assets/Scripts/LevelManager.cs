using UnityEngine;

public class LevelManager : MonoBehaviourSingleton<LevelManager> {

	public GameObject GridObj;
	public Vector2Int GridSize;
	public float CellGap;

	public CellData[,] cellData;

	void Start () 
	{
        GridCreation();
	}

    public void GridCreation()
    {
		GridDestroy();

		cellData = new CellData[GridSize.x, GridSize.y];

		Vector3 GridObjPos;
        Vector2 GridCenter;

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
				GridObjInst.AddComponent<CellData>().Initialize(new Vector2(x, y), false);
				cellData[x, y] = GridObjInst.GetComponent<CellData>();
				GridObjInst.name = "Cell_" + (x).ToString() + "." + (y).ToString();
				GridObjInst.transform.SetParent(transform);
            }
        }
    }

    public void GridDestroy()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}
