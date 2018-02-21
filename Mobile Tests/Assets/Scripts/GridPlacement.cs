using System.Collections;
using UnityEngine;

public class GridPlacement : MonoBehaviour {

	public GameObject ObjToPlace;
	public GameObject gridHighlight;

	GameObject gridHighlightInst;
	RaycastHit hitInfo, hitInfoToPlace;
	bool instantiated;

	void Update () 
	{
		if (Input.touchCount > 0 && UIManager.Instance.placeObj)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
			int layerMask = 1 << 8;

			if (Physics.Raycast(ray, out hitInfo, 1000.0f, layerMask))
			{
				GridHighlight();
			}
			ObjPlacement();
		}
	}

	void GridHighlight()
	{
		if (!hitInfo.transform.GetComponent<CellData>().objPlaced && hitInfo.normal.y > 0.3f)
		{
			if (Input.GetTouch(0).phase == TouchPhase.Began)
			{
				gridHighlightInst = Instantiate(gridHighlight, ObjOnGridPosition(gridHighlight, hitInfo), hitInfo.transform.rotation);
				hitInfoToPlace = hitInfo;
				instantiated = true;
			}
			if (Input.GetTouch(0).phase == TouchPhase.Moved)
			{
				if(instantiated == false)
				{
					gridHighlightInst = Instantiate(gridHighlight, ObjOnGridPosition(gridHighlight, hitInfo), hitInfo.transform.rotation);
					hitInfoToPlace = hitInfo;
					instantiated = true;
				}
				gridHighlightInst.transform.position = ObjOnGridPosition(gridHighlightInst, hitInfo);
				hitInfoToPlace = hitInfo;
			}
		}
	}

	void ObjPlacement()
	{
		if (Input.GetTouch(0).phase == TouchPhase.Ended && gridHighlightInst != null)
		{
			Destroy(gridHighlightInst);
			instantiated = false;
			GameObject ObjToPlaceInst = Instantiate(ObjToPlace, ObjOnGridPosition(ObjToPlace, hitInfoToPlace), hitInfoToPlace.transform.rotation);
			ObjToPlaceInst.transform.SetParent(hitInfoToPlace.transform);
			hitInfoToPlace.transform.GetComponent<CellData>().objPlaced = true;
		}
	}

	Vector3 ObjOnGridPosition(GameObject obj, RaycastHit hit)
	{
		Vector3 V3Aux = obj.transform.localScale;

		V3Aux.x = obj.transform.localScale.x * hit.normal.x;
		V3Aux.y = obj.transform.localScale.y * hit.normal.y + hit.transform.localScale.y;
		V3Aux.z = obj.transform.localScale.z * hit.normal.z;

		return hit.transform.position + V3Aux / 2f;
	}
}
