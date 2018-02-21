using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExLoadScene_B_Idle : IExLoadScene_B 
	{
		public ExLoadScene_B_Idle_Logic exLoadScene_B_Idle_Logic;
		
		public ExLoadScene_B exLoadScene_B; 

		public ExLoadScene_B_Idle (ExLoadScene_B setExLoadScene_B) 
		{
			exLoadScene_B = setExLoadScene_B;

			exLoadScene_B_Idle_Logic = new ExLoadScene_B_Idle_Logic (this);
		}

		public void StateUpdate () 
		{
			exLoadScene_B_Idle_Logic.LogicUpdate ();
		}

		public void ToIdle ()
		{
			exLoadScene_B.currentState = exLoadScene_B.idle;
		}

	}
}
