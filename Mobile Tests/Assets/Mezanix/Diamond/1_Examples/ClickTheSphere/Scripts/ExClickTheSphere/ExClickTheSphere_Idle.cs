using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExClickTheSphere_Idle : IExClickTheSphere 
	{
		public ExClickTheSphere_Idle_move exClickTheSphere_Idle_move;
		public ExClickTheSphere_Idle_clickMe exClickTheSphere_Idle_clickMe;
		
		public ExClickTheSphere exClickTheSphere; 

		public ExClickTheSphere_Idle (ExClickTheSphere setExClickTheSphere) 
		{
			exClickTheSphere = setExClickTheSphere;

			exClickTheSphere_Idle_move = new ExClickTheSphere_Idle_move (this);
			exClickTheSphere_Idle_clickMe = new ExClickTheSphere_Idle_clickMe (this);
		}

		public void StateUpdate () 
		{
			exClickTheSphere_Idle_move.LogicUpdate ();
			exClickTheSphere_Idle_clickMe.LogicUpdate ();
		}

		public void ToIdle ()
		{
			exClickTheSphere.currentState = exClickTheSphere.idle;
		}

	}
}
