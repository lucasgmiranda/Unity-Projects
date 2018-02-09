using System.Collections;
using UnityEngine;

public class tests : MonoBehaviour {

	public GameObject ObjToPlace;

	void Update () 
	{
		ObjPlacement();
	}

	void ObjPlacement()
	{
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			RaycastHit hitInfo;
			Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

			if (Physics.Raycast (ray, out hitInfo, 1000.0f)) 
			{
				if (hitInfo.transform.CompareTag("GridPlace"))
				{
					//Debug.DrawLine(Camera.main.transform.position, hitInfo.point, Color.green, 100f);
					Vector3 V3Aux = ObjToPlace.transform.localScale;

					V3Aux.x = ObjToPlace.transform.localScale.x * hitInfo.normal.x;
					V3Aux.y = ObjToPlace.transform.localScale.y * hitInfo.normal.y;
					V3Aux.z = ObjToPlace.transform.localScale.x * hitInfo.normal.z;

					Vector3 ObjToPlacePos = hitInfo.transform.position + V3Aux / 2f;

					GameObject ObjToPlaceInst = Instantiate(ObjToPlace, ObjToPlacePos, hitInfo.transform.rotation);

					ObjToPlaceInst.transform.SetParent(hitInfo.transform);
				}
			}
		}
	}	
}
