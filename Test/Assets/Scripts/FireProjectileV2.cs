using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

namespace EpicToonFX
{
	public class FireProjectileV2 : MonoBehaviour 
	{
		private RaycastHit target;	
		public GameObject[] projectiles;
		public Transform spawnPosition;
		public GameObject targetTexture;
		GameObject targetTextureInst;
		// [HideInInspector]
		public int currentProjectile = 0;
		public float speed = 1000;
		public bool debugPath;

		public struct LaunchData
		{
			public Vector3 toTarget;
			public float gSquared, b, discriminant;
		}
		LaunchData launchData;

		void Start()
		{
			targetTextureInst = Instantiate(targetTexture);
		}

		void Update () 
		{
			Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width/2, Screen.height/2));

			Debug.DrawRay(Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2)).origin, 
						  Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2)).direction * 100, Color.yellow);

			int layerMask = 1 << 9;

			if (Physics.Raycast(ray, out target, 1000f, layerMask))
			{
				targetTextureInst.transform.position = target.point + new Vector3(0, 1.5f, 0);

				CalculateLaunchData();

				if (launchData.discriminant < 0)
					targetTextureInst.GetComponent<Light>().color = Color.red;
				else
					targetTextureInst.GetComponent<Light>().color = Color.cyan;

				if (Input.touches.Length > 1)
				{
					if (Input.GetTouch(1).phase == TouchPhase.Began)
					{
						if (launchData.discriminant < 0)
						{
							Debug.Log("Target is too far away to target at this speed");
							// Abort, or fire at max speed in its general direction?
						}
						else Launch();
					}
				}
			}
		}

		void Launch()
		{
			float discRoot = Mathf.Sqrt(launchData.discriminant);

			// Highest shot with the given max speed:
			//float time_max = Mathf.Sqrt((launchData.b + discRoot) * 2f / launchData.gSquared);

			// Most direct shot with the given max speed:
			float time_min = Mathf.Sqrt((launchData.b - discRoot) * 2f / launchData.gSquared);

			// Lowest-speed arc available:
			//float time_lowEnergy = Mathf.Sqrt(Mathf.Sqrt(launchData.toTarget.sqrMagnitude * 4f / launchData.gSquared));

			float time = time_min; // choose T_max, T_min, or some T in-between like T_lowEnergy

			// Convert from time-to-target to a launch velocity:
			Vector3 velocity = launchData.toTarget / time - Physics.gravity * time / 2f;

			GameObject projectile = Instantiate(projectiles[currentProjectile], spawnPosition.position, Quaternion.identity) as GameObject;
			projectile.transform.LookAt(velocity);
			// Apply the calculated velocity (do not use force, acceleration, or impulse modes)
			projectile.GetComponent<Rigidbody>().AddForce(velocity, ForceMode.VelocityChange);			
		}

		void CalculateLaunchData()
		{
			launchData.toTarget = target.point - transform.position;

			// Set up the terms we need to solve the quadratic equations.
			launchData.gSquared = Physics.gravity.sqrMagnitude;
			launchData.b = speed * speed + Vector3.Dot(launchData.toTarget, Physics.gravity);
			launchData.discriminant = launchData.b * launchData.b - launchData.gSquared * launchData.toTarget.sqrMagnitude;
		}		
	}
}