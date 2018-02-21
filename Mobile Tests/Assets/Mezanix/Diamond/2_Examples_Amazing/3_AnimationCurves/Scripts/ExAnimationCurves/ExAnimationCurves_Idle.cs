using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExAnimationCurves_Idle : IExAnimationCurves 
	{
		public ExAnimationCurves_Idle_Logic exAnimationCurves_Idle_Logic;
		public ExAnimationCurves_Idle_SimpleAnimationNodes exAnimationCurves_Idle_SimpleAnimationNodes;
		
		public ExAnimationCurves exAnimationCurves; 

		public ExAnimationCurves_Idle (ExAnimationCurves setExAnimationCurves) 
		{
			exAnimationCurves = setExAnimationCurves;

			exAnimationCurves_Idle_Logic = new ExAnimationCurves_Idle_Logic (this);
			exAnimationCurves_Idle_SimpleAnimationNodes = new ExAnimationCurves_Idle_SimpleAnimationNodes (this);
		}

		public void StateUpdate () 
		{
			exAnimationCurves_Idle_Logic.LogicUpdate ();
			exAnimationCurves_Idle_SimpleAnimationNodes.LogicUpdate ();
		}

		public void ToIdle ()
		{
			exAnimationCurves.currentState = exAnimationCurves.idle;
		}

	}
}
