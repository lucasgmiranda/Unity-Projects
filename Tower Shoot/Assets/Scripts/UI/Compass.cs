using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour {

	public Transform north;
	[HideInInspector]
	public bool compassHit;
	[HideInInspector]
	public Vector3 arrowDir;
	int currentFinger = -1;

	Transform arrow;

	void Start()
	{
		arrow = transform.GetChild(0);

		arrowDir = new Vector3(-arrow.right.x, 0, -arrow.right.z);
	}

	void Update () 
	{
		Vector3 compassTarget = new Vector3(north.position.x, transform.position.y, north.position.z);
		transform.LookAt(compassTarget);

		transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 90, transform.eulerAngles.z);

		CheckHit();

		if(compassHit)
		{
			float deltaX = Input.GetTouch(currentFinger).deltaPosition.x;

			arrow.RotateAround(arrow.position, Vector3.up, deltaX);

			arrowDir = new Vector3(-arrow.right.x, 0, -arrow.right.z); 
		}
	}

	void CheckHit()
	{
		foreach(Touch touch in Input.touches)
		{
			if(touch.phase == TouchPhase.Began && currentFinger == -1)
			{
				currentFinger = touch.fingerId;

				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(touch.position);
				
				if (Physics.Raycast(ray, out hit, 10)) 
				{
					if(hit.transform == transform)
					{
						compassHit = true;
					}
				}
			}
			if (touch.phase == TouchPhase.Ended && currentFinger == touch.fingerId)
			{
				compassHit = false;
				currentFinger = -1;
			}
		}
	}	
}


