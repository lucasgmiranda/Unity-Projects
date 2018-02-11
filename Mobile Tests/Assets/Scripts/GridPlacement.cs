using System.Collections;
using UnityEngine;

public class GridPlacement : MonoBehaviour {

	public GameObject ObjToPlace;
	public GameObject gridHighlight;
	RaycastHit hitInfo;

	void Update () 
	{
		if (Input.touchCount > 0)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
			int layerMask = 1 << 8;

			if (Physics.Raycast(ray, out hitInfo, 1000.0f, layerMask))
			{
				ObjPlacement();
			}
			DestroyGridHighlightInst();
		}
	}

	GameObject gridHighlightInst;

	void ObjPlacement()
	{
		if ((!GameObject.Find("GridPlace").GetComponent<GridGenerator>().gridCells[hitInfo.transform.name]) && hitInfo.normal.y > 0.3f)
		{
			if (Input.GetTouch(0).phase == TouchPhase.Began)
			{
				gridHighlightInst = Instantiate(gridHighlight, ObjOnGridPosition(gridHighlight), hitInfo.transform.rotation);
			}
			if (Input.GetTouch(0).phase == TouchPhase.Moved)
			{
				gridHighlightInst.transform.position = ObjOnGridPosition(gridHighlightInst);
			}
			if (Input.GetTouch(0).phase == TouchPhase.Ended)
			{
				Destroy(gridHighlightInst);

				GameObject ObjToPlaceInst = Instantiate(ObjToPlace, ObjOnGridPosition(ObjToPlace), hitInfo.transform.rotation);
				ObjToPlaceInst.transform.SetParent(hitInfo.transform);
				GameObject.Find("GridPlace").GetComponent<GridGenerator>().gridCells[hitInfo.transform.name] = true;
			}
		}
		DestroyGridHighlightInst();
	}

	Vector3 ObjOnGridPosition(GameObject obj)
	{
		Vector3 V3Aux = obj.transform.localScale;

		V3Aux.x = obj.transform.localScale.x * hitInfo.normal.x;
		V3Aux.y = obj.transform.localScale.y * hitInfo.normal.y + hitInfo.transform.localScale.y;
		V3Aux.z = obj.transform.localScale.z * hitInfo.normal.z;

		return hitInfo.transform.position + V3Aux / 2f;
	}

	void DestroyGridHighlightInst()
	{
		if (gridHighlightInst != null && Input.GetTouch(0).phase == TouchPhase.Ended)
			Destroy(gridHighlightInst);
	}
}
