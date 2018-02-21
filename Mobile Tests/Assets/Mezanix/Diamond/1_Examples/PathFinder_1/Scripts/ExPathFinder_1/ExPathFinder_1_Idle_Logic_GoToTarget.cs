using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExPathFinder_1_Idle_Logic_GoToTarget 
	{
		ExPathFinder_1_Idle_Logic exPathFinder_1_Idle_Logic;

		UnityEngine.AI.NavMeshAgent navMeshAgent = null;

		public bool doIT = false;

		ScriptsCreatedByDiamond.IdentifiedObjects identifiedObjects = null;

		public GameObject gameObjectValue = null;

		public GameObject [] gameObjectValues = new GameObject[2];

		public Vector3 [] vector3Values = new Vector3[2];

		public ExPathFinder_1_Idle_Logic_GoToTarget (ExPathFinder_1_Idle_Logic setExPathFinder_1_Idle_Logic) 
		{
			exPathFinder_1_Idle_Logic = setExPathFinder_1_Idle_Logic;

			exPathFinder_1_Idle_Logic.IAmHere ();

			identifiedObjects = GameObject.Find (ScriptsCreatedByDiamond.IdentifiedObjectsActions.gameObjectHolderName).GetComponent<ScriptsCreatedByDiamond.IdentifiedObjects>();

			if (identifiedObjects != null)
			{
				gameObjectValues [0] = (GameObject)identifiedObjects.GetIdentifiedObject ("8_6_2017_9_02_44_AM_7822.gameObjectValues_0");
			}

			vector3Values [0] = new Vector3 (0f, 0f, 0f);
			vector3Values [1] = new Vector3 (0f, 0f, 0f);

		}

		public void LogicNodeUpdate ()
		{
			gameObjectValues [0] = exPathFinder_1_Idle_Logic.exPathFinder_1_Idle.exPathFinder_1.attachedToGameObject;

			vector3Values [0] = exPathFinder_1_Idle_Logic.exPathFinder_1_Idle_Logic_Target.vector3Value;

			doIT = exPathFinder_1_Idle_Logic.exPathFinder_1_Idle_Logic_Or.boolValue;

			ComputeNavMeshAgent ();
		}

		void GameObjectCheck ()
		{
			if ( ! doIT)
			{
				return;
			}

			if (gameObjectValues [0] == null)
			{
				doIT = false;
			}
		}

		void NavMeshAgentCheck ()
		{
			if ( ! doIT)
			{
				return;
			}

			navMeshAgent = gameObjectValues [0].GetComponent <UnityEngine.AI.NavMeshAgent> ();

			if (navMeshAgent == null)
			{
				doIT = false;
			}
		}

		void ComputeNavMeshAgent ()
		{
			GameObjectCheck ();

			NavMeshAgentCheck ();

			if ( ! doIT)
			{
				return;
			}

			gameObjectValue = gameObjectValues [0];

			navMeshAgent.destination = vector3Values [0];

		}
	}
}
