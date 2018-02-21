using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExPathFinder_1_Idle_Logic_RemainingDistance 
	{
		ExPathFinder_1_Idle_Logic exPathFinder_1_Idle_Logic;

		UnityEngine.AI.NavMeshAgent navMeshAgent = null;

		public bool doIT = false;

		ScriptsCreatedByDiamond.IdentifiedObjects identifiedObjects = null;

		public GameObject gameObjectValue = null;

		public GameObject [] gameObjectValues = new GameObject[2];

		public float floatValue = 0f;

		public ExPathFinder_1_Idle_Logic_RemainingDistance (ExPathFinder_1_Idle_Logic setExPathFinder_1_Idle_Logic) 
		{
			exPathFinder_1_Idle_Logic = setExPathFinder_1_Idle_Logic;

			exPathFinder_1_Idle_Logic.IAmHere ();

			identifiedObjects = GameObject.Find (ScriptsCreatedByDiamond.IdentifiedObjectsActions.gameObjectHolderName).GetComponent<ScriptsCreatedByDiamond.IdentifiedObjects>();

			if (identifiedObjects != null)
			{
				gameObjectValues [0] = (GameObject)identifiedObjects.GetIdentifiedObject ("8_6_2017_8_52_17_AM_8401.gameObjectValues_0");
			}

		}

		public void LogicNodeUpdate ()
		{
			gameObjectValues [0] = exPathFinder_1_Idle_Logic.exPathFinder_1_Idle.exPathFinder_1.attachedToGameObject;

			doIT = true;

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

			floatValue = navMeshAgent.remainingDistance;

		}
	}
}
