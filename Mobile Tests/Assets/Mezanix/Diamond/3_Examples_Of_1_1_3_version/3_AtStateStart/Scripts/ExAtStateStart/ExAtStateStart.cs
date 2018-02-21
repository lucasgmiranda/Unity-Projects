using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExAtStateStart : MonoBehaviour 
	{
		[HideInInspector] 
		public IExAtStateStart currentState; 

		[HideInInspector] 
		public ExAtStateStart_Idle idle; 

		[HideInInspector] 
		public ExAtStateStart_Counter counter; 

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
			idle = new ExAtStateStart_Idle (this);
			counter = new ExAtStateStart_Counter (this);
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
