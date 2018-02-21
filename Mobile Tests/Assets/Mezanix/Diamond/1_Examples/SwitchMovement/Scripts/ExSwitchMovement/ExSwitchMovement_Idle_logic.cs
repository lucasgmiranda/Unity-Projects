using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExSwitchMovement_Idle_logic 
	{
		public ExSwitchMovement_Idle_logic_goToMoveRight exSwitchMovement_Idle_logic_goToMoveRight;
		
		public ExSwitchMovement_Idle exSwitchMovement_Idle;

		public ExSwitchMovement_Idle_logic (ExSwitchMovement_Idle setExSwitchMovement_Idle)
		{
			exSwitchMovement_Idle = setExSwitchMovement_Idle;

			exSwitchMovement_Idle_logic_goToMoveRight = new ExSwitchMovement_Idle_logic_goToMoveRight (this);
		}

		public void LogicUpdate ()
		{
			exSwitchMovement_Idle_logic_goToMoveRight.LogicNodeUpdate ();
		}

		public void IAmHere ()
		{
		}	}
}
