using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExMoveCursor_Idle : IExMoveCursor 
	{
		public ExMoveCursor_Idle_logic exMoveCursor_Idle_logic;
		
		public ExMoveCursor exMoveCursor; 

		public ExMoveCursor_Idle (ExMoveCursor setExMoveCursor) 
		{
			exMoveCursor = setExMoveCursor;

			exMoveCursor_Idle_logic = new ExMoveCursor_Idle_logic (this);
		}

		public void StateUpdate () 
		{
			exMoveCursor_Idle_logic.LogicUpdate ();
		}

		public void ToIdle ()
		{
			exMoveCursor.currentState = exMoveCursor.idle;
		}

	}
}
