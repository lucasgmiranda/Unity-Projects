using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExSwitchMovement : MonoBehaviour 
	{
		[HideInInspector] 
		public IExSwitchMovement currentState; 

		[HideInInspector] 
		public ExSwitchMovement_Idle idle; 

		[HideInInspector] 
		public ExSwitchMovement_moveRight moveRight; 

		[HideInInspector] 
		public ExSwitchMovement_moveLeft moveLeft; 

		[HideInInspector]
		public GameObject attachedToGameObject;

		[HideInInspector]
		public GameObject gameObjectsFoundBytrigger = null;

		[HideInInspector]
		public int gameObjectsFoundBytriggerIndex = -1;

		
		void Awake () 
		{ 
			ProjectVariables.Init ();

			attachedToGameObject = gameObject;
			idle = new ExSwitchMovement_Idle (this);
			moveRight = new ExSwitchMovement_moveRight (this);
			moveLeft = new ExSwitchMovement_moveLeft (this);
		} 

		void Start () 
		{ 
			currentState = idle; 

		} 

		void Update () 
		{ 
			currentState.StateUpdate (); 
		} 

		void LateUpdate ()
		{
			FreeameObjectsFoundBytriggerIndex ();
		}

		void FreeameObjectsFoundBytriggerIndex ()
		{
			if (gameObjectsFoundBytrigger == null)
			{
				gameObjectsFoundBytriggerIndex = -1;
			}
		}

	}
}
