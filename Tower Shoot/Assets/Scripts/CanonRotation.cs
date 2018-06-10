using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonRotation : MonoBehaviour {

	FireProjectileV2 FP;
	Transform cano;

	private void Start()
	{
		FP = GameObject.Find("Launch Source").GetComponent<FireProjectileV2>();
		cano = GameObject.Find("Cano").transform;
	}

	void LateUpdate ()
	{
		Vector3 towerTargetPostition = new Vector3(FP.targetHit.point.x, transform.position.y, FP.targetHit.point.z);
		transform.LookAt(towerTargetPostition);

		if (FP.launchData.discriminant >= 1)
		{
			cano.LookAt(FP.launchData.velocity);
			cano.rotation *= Quaternion.Euler(80, 0, 0);
		}
	}
}
