using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class FireProjectileV2 : MonoBehaviour 
{
	public RaycastHit targetHit;
	public GameObject raycaster;
	public GameObject[] projectiles;
	public Transform spawnPosition;
	public GameObject targetTexture;
	public int currentProjectile = 0;
	public float maxSpeed = 1000, lauchDelay;
	public float maxWindForce;
	[Range(0, 2)]
	public int equationType;
	Vector3 wind;	
	float windForce, speed;

	[HideInInspector]
	public bool projectileLaunched;

	GameObject targetTextureInst;
	StaminaSystem stamina;
	UIButtonData fireButton;
	ConfigButtonsUI CB;
	Compass CP;

	public struct LaunchData
	{
		public Vector3 toTarget, velocity;
		public float gravitySquared, b, discriminant;
	}
	public LaunchData launchData;

	void Start()
	{
		targetTextureInst = Instantiate(targetTexture);
		stamina = GameObject.Find("Stamina Bar").GetComponent<StaminaSystem>();
		fireButton = GameObject.Find("Fire Button").GetComponent<UIButtonData>();
		CP = FindObjectOfType<Compass>();
		CB = FindObjectOfType<ConfigButtonsUI>();

		AdjustFireSpeed(0.5f);
		AdjustEquation(0);
	}

	void Update () 
	{
		projectileLaunched = false;

		RaycastHit cameraHit;
		Vector3 raycastOrigin = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - 1f, Camera.main.transform.position.z);

		if (Physics.Raycast(raycastOrigin, Camera.main.transform.forward, out cameraHit, 100.0f, 1 << 10))
		{
			raycaster.transform.position = new Vector3(cameraHit.point.x, raycaster.transform.position.y, cameraHit.point.z);
		}

		Debug.DrawRay(raycastOrigin, Camera.main.transform.forward * 100, Color.yellow);
		Debug.DrawRay(raycaster.transform.position, -Vector3.up * 100, Color.red);

		if (Physics.Raycast(raycaster.transform.position, -Vector3.up, out targetHit, 20f, 1<<9))
		{
			
			targetTextureInst.transform.position = targetHit.point;
			targetTextureInst.transform.rotation = Quaternion.FromToRotation(targetTextureInst.transform.up, targetHit.normal) * targetTextureInst.transform.rotation;

			CalculateLaunchData();

			if (fireButton._click)
			{
				fireButton._click = false;
				if (launchData.discriminant > 0 && stamina.canFire(1f))
				{
					StartCoroutine(LaunchDelay());
					stamina.decreaseValue(0.6f);
					projectileLaunched = true;
				}				
			}
		}
	}	
	
	void Launch()
	{
		GameObject projectile = Instantiate(projectiles[currentProjectile], transform.position, Quaternion.identity) as GameObject;
		// Apply the calculated velocity (do not use force, acceleration, or impulse modes)
		projectile.GetComponent<Rigidbody>().AddForce(launchData.velocity, ForceMode.VelocityChange);			
	}

	IEnumerator LaunchDelay()
	{
		yield return new WaitForSeconds(lauchDelay);
		Launch();		
	}

	void CalculateLaunchData()
	{
		wind = CP.arrowDir * windForce;
		Physics.gravity = new Vector3(0, -9.81f, 0) + wind;

		launchData.toTarget = targetHit.point - transform.position;

		// Set up the terms we need to solve the quadratic equations.
		launchData.gravitySquared = Physics.gravity.sqrMagnitude;
		launchData.b = speed * speed + Vector3.Dot(launchData.toTarget, Physics.gravity);
		launchData.discriminant = launchData.b * launchData.b - launchData.gravitySquared * launchData.toTarget.sqrMagnitude;

		if (launchData.discriminant >= 0)
		{
			float time = 0;
			float discRoot = Mathf.Sqrt(launchData.discriminant);

			switch (equationType)
			{
				case 0: // Tiro com a menor velocidade possível:
					time = Mathf.Sqrt(Mathf.Sqrt(launchData.toTarget.sqrMagnitude * 4f / launchData.gravitySquared));
					break;				 
				case 1:	// Tiro mais direto com a velocidade máxima:
					time = Mathf.Sqrt((launchData.b - discRoot) * 2f / launchData.gravitySquared);
					break;
				case 2: // Tiro mais alto com a velocidade máxima:					
					time = Mathf.Sqrt((launchData.b + discRoot) * 2f / launchData.gravitySquared);
					break;
			}
			// Converte o tempo até o alvo para a velocidade de lançamento:
			launchData.velocity = launchData.toTarget / time - Physics.gravity * time / 2f;

			targetTextureInst.GetComponentInChildren<Light>().color = Color.cyan;
		}
		else targetTextureInst.GetComponentInChildren<Light>().color = Color.red;
	}

	public void AdjustWindForce(float multiplier)
	{
		windForce = maxWindForce * multiplier;
		CB.GetButton("Wind Button").text.text = String.Format("{0:0.0}", windForce);
		FindObjectOfType<AudioManager>().Volume("Heavy Rain", multiplier);
	}

	public void AdjustFireSpeed(float multiplier)
	{
		speed = maxSpeed * multiplier;
		CB.GetButton("Force Button").text.text = String.Format("{0:0.0}", speed);
	}

	public void AdjustEquation(float value)
	{
		equationType = (int)value;

		switch (equationType)
		{
			case 0: // Tiro mais alto com a velocidade máxima:
				CB.GetButton("Equation Button").text.text = "A";
				break;
			case 1: // Tiro mais direto com a velocidade máxima:
				CB.GetButton("Equation Button").text.text = "B";
				break;
			case 2: // Tiro com a menor velocidade possível:
				CB.GetButton("Equation Button").text.text = "C";
				break;
		}
	}
}