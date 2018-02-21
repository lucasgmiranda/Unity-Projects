using UnityEngine;
using System.Collections;

public class RaycastTest : MonoBehaviour 
{
	public Transform gunObj;
	void Update() 
	{
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) 
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

			if (Physics.Raycast(ray, out hit)) 
			{
				Vector3 incomingVec = hit.point - gunObj.position;
				Vector3 reflectVec = Vector3.Reflect(incomingVec, hit.normal);
				Debug.Log(incomingVec);
				Debug.Log(hit.normal);
				Debug.Log(reflectVec);
				Debug.DrawLine(gunObj.position, hit.point, Color.red, 2f);
				Debug.DrawRay(hit.point, reflectVec, Color.green, 2f);
			}
		}
	}
}