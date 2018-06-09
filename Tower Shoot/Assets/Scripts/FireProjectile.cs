using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

namespace EpicToonFX
{
	public class FireProjectile : MonoBehaviour 
	{
		private RaycastHit hit;		
		private GameObject projectile;
		public GameObject[] projectiles;
		public Transform spawnPosition;
		// [HideInInspector]
		public int currentProjectile = 0;
		public float h = 10;
		public float gravity = -18;
		public float speed = 1000;
		public bool debugPath;

		void Start () 
		{
			//selectedProjectileButton = GameObject.Find("Button").GetComponent<ETFXButtonScript>();
		}

		void Update () 
		{
			for (int i = 0; i < Input.touchCount; ++i)
			{
				if (Input.GetTouch(0).phase == TouchPhase.Began)
				{
					Ray ray;

					if (Input.touchSupported)
						ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
					else
						ray = Camera.main.ScreenPointToRay(Input.mousePosition);

					if (Physics.Raycast(ray, out hit, 1000f))
					{
						projectile = Instantiate(projectiles[currentProjectile], spawnPosition.position, Quaternion.identity) as GameObject;
						//projectile.transform.LookAt(hit.point);
						if (debugPath)
						{
							if (projectile != null)
								DrawPath();
						}
						Launch();
					}
				}
			}
			
			//Debug.DrawRay(Camera.main.ScreenPointToRay(Input.mousePosition).origin, Camera.main.ScreenPointToRay(Input.mousePosition).direction*100, Color.yellow);
		}

		void Launch()
		{
			Physics.gravity = Vector3.up * gravity;
			projectile.GetComponent<Rigidbody>().useGravity = true;
			projectile.GetComponent<Rigidbody>().velocity = CalculateLaunchData().initialVelocity;
		}

		LaunchData CalculateLaunchData()
		{
			float displacementY = hit.point.y - projectile.transform.position.y;
			Vector3 displacementXZ = new Vector3(hit.point.x - projectile.transform.position.x, 0, hit.point.z - projectile.transform.position.z);
			float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);
			Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
			Vector3 velocityXZ = displacementXZ / time;

			return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
		}

		void DrawPath()
		{
			LaunchData launchData = CalculateLaunchData();
			Vector3 previousDrawPoint = projectile.transform.position;

			int resolution = 30;
			for (int i = 1; i <= resolution; i++)
			{
				float simulationTime = i / (float)resolution * launchData.timeToTarget;
				Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
				Vector3 drawPoint = projectile.transform.position + displacement;
				Debug.DrawLine(previousDrawPoint, drawPoint, Color.green, 5);
				previousDrawPoint = drawPoint;
			}
		}

		public struct LaunchData
		{
			public readonly Vector3 initialVelocity;
			public readonly float timeToTarget;

			public LaunchData(Vector3 initialVelocity, float timeToTarget)
			{
				this.initialVelocity = initialVelocity;
				this.timeToTarget = timeToTarget;
			}

		}
	}
}