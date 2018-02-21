using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExSwitchMovement_Idle_logic_goToMoveRight 
	{
		ExSwitchMovement_Idle_logic exSwitchMovement_Idle_logic;

		public bool [] boolValues = new bool[2];

		public ExSwitchMovement_Idle_logic_goToMoveRight (ExSwitchMovement_Idle_logic setExSwitchMovement_Idle_logic) 
		{
			exSwitchMovement_Idle_logic = setExSwitchMovement_Idle_logic;

			exSwitchMovement_Idle_logic.IAmHere ();

			boolValues [0] = true;
			boolValues [1] = false;
		}

		public void LogicNodeUpdate ()
		{
			GoToState ();
		}

		void GoToState ()
		{
			if ( ! boolValues [0])
			{
				return; 
			}

			exSwitchMovement_Idle_logic.exSwitchMovement_Idle.TomoveRight ();

		}

	}
}
