using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExKeplerOrbit_Idle : IExKeplerOrbit 
	{
		public ExKeplerOrbit_Idle_Logic exKeplerOrbit_Idle_Logic;
		
		public ExKeplerOrbit exKeplerOrbit; 

		public ExKeplerOrbit_Idle (ExKeplerOrbit setExKeplerOrbit) 
		{
			exKeplerOrbit = setExKeplerOrbit;

			exKeplerOrbit_Idle_Logic = new ExKeplerOrbit_Idle_Logic (this);
		}

		public void StateUpdate () 
		{
			exKeplerOrbit_Idle_Logic.LogicUpdate ();
		}

		public void ToIdle ()
		{
			exKeplerOrbit.currentState = exKeplerOrbit.idle;
		}

	}
}
