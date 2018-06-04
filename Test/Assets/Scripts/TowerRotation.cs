using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRotation : MonoBehaviour {

	// Update is called once per frame
	void Update ()
	{
		Vector3 target = GameObject.Find("Launch Source").GetComponent<FireProjectileV2>().targetHit.point;

		Vector3 targetPostition = new Vector3(target.x, transform.position.y, target.z);
		transform.LookAt(targetPostition);
	}
}
