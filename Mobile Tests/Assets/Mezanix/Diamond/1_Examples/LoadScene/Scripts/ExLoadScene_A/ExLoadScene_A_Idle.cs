using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExLoadScene_A_Idle : IExLoadScene_A 
	{
		public ExLoadScene_A_Idle_Logic exLoadScene_A_Idle_Logic;
		
		public ExLoadScene_A exLoadScene_A; 

		public ExLoadScene_A_Idle (ExLoadScene_A setExLoadScene_A) 
		{
			exLoadScene_A = setExLoadScene_A;

			exLoadScene_A_Idle_Logic = new ExLoadScene_A_Idle_Logic (this);
		}

		public void StateUpdate () 
		{
			exLoadScene_A_Idle_Logic.LogicUpdate ();
		}

		public void ToIdle ()
		{
			exLoadScene_A.currentState = exLoadScene_A.idle;
		}

	}
}
