using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExMoveSphere_Idle : IExMoveSphere 
	{
		public ExMoveSphere_Idle_Logic exMoveSphere_Idle_Logic;
		
		public ExMoveSphere exMoveSphere; 

		public ExMoveSphere_Idle (ExMoveSphere setExMoveSphere) 
		{
			exMoveSphere = setExMoveSphere;

			exMoveSphere_Idle_Logic = new ExMoveSphere_Idle_Logic (this);
		}

		public void StateUpdate () 
		{
			exMoveSphere_Idle_Logic.LogicUpdate ();
		}

		public void ToIdle ()
		{
			exMoveSphere.currentState = exMoveSphere.idle;
		}

	}
}
