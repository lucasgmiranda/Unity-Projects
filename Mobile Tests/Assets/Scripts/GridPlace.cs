using System.Collections;
using UnityEngine;

public class GridPlace : MonoBehaviour {

	[SerializeField]
	private GameObject GridObj;
	[SerializeField]
	private Vector2Int GridSize;
	[SerializeField]
	private float CellGap;

	void Start () 
	{
		Vector3 GridObjPos;
		Vector2 GridCenter;

		GridCenter.x = this.transform.position.x - (GridSize.x - 1) * (GridObj.transform.localScale.x + CellGap) / 2;
		GridCenter.y = this.transform.position.z - (GridSize.y - 1) * (GridObj.transform.localScale.z + CellGap) / 2;

		Debug.Log(GridCenter.y);
		Debug.Log(GridCenter.x);

		for (int y = 0; y < GridSize.y; y++)
		{
			for (int x = 0; x < GridSize.x; x++) 
			{
				GridObjPos.x = x * (GridObj.transform.localScale.x + CellGap) + GridCenter.x;
				GridObjPos.y = this.transform.position.y;
				GridObjPos.z = y * (GridObj.transform.localScale.z + CellGap) + GridCenter.y;

				GameObject GridObjInst = Instantiate(GridObj, GridObjPos, Quaternion.identity);

				GridObjInst.transform.SetParent(this.transform);
			}
		}
	}
}
