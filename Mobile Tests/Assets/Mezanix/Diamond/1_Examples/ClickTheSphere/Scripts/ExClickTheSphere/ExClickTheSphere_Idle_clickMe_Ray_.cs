using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExClickTheSphere_Idle_clickMe_Ray_ 
	{
		ExClickTheSphere_Idle_clickMe exClickTheSphere_Idle_clickMe;

		public bool doIT = false;

		public Vector3 raycastHitBarycentricCoordinate;

		public int raycastHitTriangleIndex;


		public Vector3 raycastHitPoint;

		public Vector3 raycastHitNormal;

		public float raycastHitDistance;


		public GameObject raycastHitGameObject;


		public Vector2 raycastHitLightmapCoord;

		public Vector2 raycastHittextureCoord;

		public Vector2 raycastHittextureCoord2;

		public bool boolValue = false;

		RaycastHit hit;

		public Vector3 [] vector3Values = new Vector3[2];

		public float [] floatValues = new float[3];

		public int [] intValues = new int[3];

		QueryTriggerInteraction queryTriggerInteraction;

		public ExClickTheSphere_Idle_clickMe_Ray_ (ExClickTheSphere_Idle_clickMe setExClickTheSphere_Idle_clickMe) 
		{
			exClickTheSphere_Idle_clickMe = setExClickTheSphere_Idle_clickMe;

			exClickTheSphere_Idle_clickMe.IAmHere ();

			vector3Values [0] = new Vector3 (2.210699f, 7.84153f, -9.700012f);
			vector3Values [1] = new Vector3 (0f, 0f, 1f);

			floatValues [0] = 15f;
			floatValues [1] = 1f;
			floatValues [2] = 1f;

			intValues [0] = -1;
			intValues [1] = 0;
			intValues [2] = 0;

			queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;

		}

		public void LogicNodeUpdate ()
		{
			vector3Values [0] = exClickTheSphere_Idle_clickMe.exClickTheSphere_Idle_clickMe_ScreenPointToRay.rayValueOrigin;

			vector3Values [1] = exClickTheSphere_Idle_clickMe.exClickTheSphere_Idle_clickMe_ScreenPointToRay.rayDirectionValueNotNormalized;

			doIT = exClickTheSphere_Idle_clickMe.exClickTheSphere_Idle_clickMe_LeftClick.boolValue;

			ComputeRay ();
		}

		void ResetRayCastHitInfos ()
		{
			boolValue = false;

			raycastHitGameObject = null;



			raycastHitBarycentricCoordinate = new Vector3 ();

			raycastHitTriangleIndex = -1;


			raycastHitPoint = new Vector3 ();

			raycastHitNormal = new Vector3 ();

			raycastHitDistance = 0f;


			raycastHitLightmapCoord = new Vector2 ();

			raycastHittextureCoord = new Vector2 ();

			raycastHittextureCoord2 =  new Vector2 ();
		}

		void AssignRayCasthitInfos ()
		{
			if ( ! boolValue)
			{
				ResetRayCastHitInfos ();

				return;
			}

			raycastHitGameObject = hit.transform.gameObject;

			if (raycastHitGameObject == null)
				raycastHitGameObject = hit.collider.gameObject;

			if (raycastHitGameObject == null)
				raycastHitGameObject = hit.rigidbody.gameObject;



			raycastHitBarycentricCoordinate = hit.barycentricCoordinate;

			raycastHitTriangleIndex = hit.triangleIndex;


			raycastHitPoint = hit.point;

			raycastHitNormal = hit.normal;

			raycastHitDistance = hit.distance;


			raycastHitLightmapCoord = hit.lightmapCoord;

			raycastHittextureCoord = hit.textureCoord;

			raycastHittextureCoord2 =  hit.textureCoord2;
		}

		void ComputeRay ()
		{
			ResetRayCastHitInfos ();

			if ( ! doIT)
			{
				return;
			}


			boolValue = Physics.Raycast (vector3Values [0], vector3Values [1],
				out hit, floatValues [0], intValues [0], queryTriggerInteraction);

			AssignRayCasthitInfos ();

		}
	}
}
