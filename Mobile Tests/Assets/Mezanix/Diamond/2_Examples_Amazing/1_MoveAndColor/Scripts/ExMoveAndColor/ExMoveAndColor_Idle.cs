using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExMoveAndColor_Idle : IExMoveAndColor 
	{
		public ExMoveAndColor_Idle_Move exMoveAndColor_Idle_Move;
		public ExMoveAndColor_Idle_ColorOnPosition exMoveAndColor_Idle_ColorOnPosition;
		
		public ExMoveAndColor exMoveAndColor; 

		public ExMoveAndColor_Idle (ExMoveAndColor setExMoveAndColor) 
		{
			exMoveAndColor = setExMoveAndColor;

			exMoveAndColor_Idle_Move = new ExMoveAndColor_Idle_Move (this);
			exMoveAndColor_Idle_ColorOnPosition = new ExMoveAndColor_Idle_ColorOnPosition (this);
		}

		public void StateUpdate () 
		{
			exMoveAndColor_Idle_Move.LogicUpdate ();
			exMoveAndColor_Idle_ColorOnPosition.LogicUpdate ();
		}

		public void ToIdle ()
		{
			exMoveAndColor.currentState = exMoveAndColor.idle;
		}

	}
}
