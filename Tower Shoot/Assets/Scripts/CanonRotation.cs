using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonRotation : MonoBehaviour
{
	LaunchArc LA;
	FireProjectileV2 FP;
	Transform barrelMain, barrelTop;
	Vector3 initialPos;
	Vector3 recoilSmoothDampVelocity;
	float recoilAngle, recoilRotSmoothDampVelocity;

	private void Start()
	{
		FP = GameObject.Find("Launch Source").GetComponent<FireProjectileV2>();
		LA = GameObject.Find("Launch Source").GetComponent<LaunchArc>();
		barrelMain = GameObject.Find("Barrel_Main").transform;
		barrelTop = GameObject.Find("Barrel_Top").transform;
		initialPos = barrelTop.localPosition;
	}

	void LateUpdate()
	{		
		Vector3 towerTargetLook = new Vector3(FP.targetHit.point.x, transform.position.y, FP.targetHit.point.z);
		transform.LookAt(towerTargetLook);

		barrelMain.LookAt(LA.canonTargetLook);
		
		recoilAngle = Mathf.SmoothDamp(recoilAngle, 0, ref recoilRotSmoothDampVelocity, 0.2f);
		barrelMain.localEulerAngles = barrelMain.localEulerAngles + Vector3.left * recoilAngle;

		barrelTop.localPosition = Vector3.SmoothDamp(barrelTop.localPosition, initialPos, ref recoilSmoothDampVelocity, 0.1f);

		if (FP.projectileLaunched)
		{
			CanonRecoil();
		}
		
	}

	void CanonRecoil()
	{
		barrelTop.position -= barrelTop.forward * 0.3f;
		recoilAngle += 7;
		recoilAngle = Mathf.Clamp(recoilAngle, 0, 45);
	}
}