using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExLoadScene_Idle : IExLoadScene 
	{
		
		public ExLoadScene exLoadScene; 

		public ExLoadScene_Idle (ExLoadScene setExLoadScene) 
		{
			exLoadScene = setExLoadScene;

		}

		public void StateUpdate () 
		{
			Debug.Log ("StateUpdate");
		}

		public void ToIdle ()
		{
			exLoadScene.currentState = exLoadScene.idle;
		}

	}
}
