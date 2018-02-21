using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExPathFinder_1_Idle : IExPathFinder_1 
	{
		public ExPathFinder_1_Idle_Logic exPathFinder_1_Idle_Logic;
		
		public ExPathFinder_1 exPathFinder_1; 

		public ExPathFinder_1_Idle (ExPathFinder_1 setExPathFinder_1) 
		{
			exPathFinder_1 = setExPathFinder_1;

			exPathFinder_1_Idle_Logic = new ExPathFinder_1_Idle_Logic (this);
		}

		public void StateUpdate () 
		{
			exPathFinder_1_Idle_Logic.LogicUpdate ();
		}

		public void ToIdle ()
		{
			exPathFinder_1.currentState = exPathFinder_1.idle;
		}

	}
}
