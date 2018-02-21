using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExZoom : MonoBehaviour 
	{
		[HideInInspector] 
		public IExZoom currentState; 

		[HideInInspector] 
		public ExZoom_Idle idle; 

		[HideInInspector] 
		public ExZoom_deZoom deZoom; 

		[HideInInspector] 
		public ExZoom_zoom zoom; 

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
			idle = new ExZoom_Idle (this);
			deZoom = new ExZoom_deZoom (this);
			zoom = new ExZoom_zoom (this);
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
