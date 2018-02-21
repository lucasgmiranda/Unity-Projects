using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExMoveCursor_Idle_logic 
	{
		public ExMoveCursor_Idle_logic_mousePosition exMoveCursor_Idle_logic_mousePosition;
		public ExMoveCursor_Idle_logic_offset_Z exMoveCursor_Idle_logic_offset_Z;
		public ExMoveCursor_Idle_logic_screenToWorldPoint exMoveCursor_Idle_logic_screenToWorldPoint;
		public ExMoveCursor_Idle_logic_UpdateCursorPosition exMoveCursor_Idle_logic_UpdateCursorPosition;
		
		public ExMoveCursor_Idle exMoveCursor_Idle;

		public ExMoveCursor_Idle_logic (ExMoveCursor_Idle setExMoveCursor_Idle)
		{
			exMoveCursor_Idle = setExMoveCursor_Idle;

			exMoveCursor_Idle_logic_mousePosition = new ExMoveCursor_Idle_logic_mousePosition (this);
			exMoveCursor_Idle_logic_offset_Z = new ExMoveCursor_Idle_logic_offset_Z (this);
			exMoveCursor_Idle_logic_screenToWorldPoint = new ExMoveCursor_Idle_logic_screenToWorldPoint (this);
			exMoveCursor_Idle_logic_UpdateCursorPosition = new ExMoveCursor_Idle_logic_UpdateCursorPosition (this);
		}

		public void LogicUpdate ()
		{
			exMoveCursor_Idle_logic_mousePosition.LogicNodeUpdate ();
			exMoveCursor_Idle_logic_offset_Z.LogicNodeUpdate ();
			exMoveCursor_Idle_logic_screenToWorldPoint.LogicNodeUpdate ();
			exMoveCursor_Idle_logic_UpdateCursorPosition.LogicNodeUpdate ();
		}

		public void IAmHere ()
		{
		}	}
}
