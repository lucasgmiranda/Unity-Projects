using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearsTest : MonoBehaviour {

	public Transform gear1, gear2;
	Vector3 previousFoward;
	float frameCont = 0.6f;

	private void Start()
	{
		previousFoward = gear1.forward;
	}

	void Update ()
	{
		if (frameCont > 0.05f)
		{
			Vector3 deltaAngle = DeltaAngle(gear1);
			gear2.Rotate(Vector3.up * 4 * deltaAngle.y);
			frameCont = 0f;
		}

		frameCont += Time.deltaTime;
	}

	Vector3 DeltaAngle(Transform obj)
	{
		Vector3 deltaAngle = Quaternion.FromToRotation(obj.forward, previousFoward).eulerAngles;
		previousFoward = obj.forward;

		return deltaAngle;
	}
}
