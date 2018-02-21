using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExSwitchMovement_moveLeft_logic_goToMoveLeft 
	{
		ExSwitchMovement_moveLeft_logic exSwitchMovement_moveLeft_logic;

		public bool [] boolValues = new bool[2];

		public ExSwitchMovement_moveLeft_logic_goToMoveLeft (ExSwitchMovement_moveLeft_logic setExSwitchMovement_moveLeft_logic) 
		{
			exSwitchMovement_moveLeft_logic = setExSwitchMovement_moveLeft_logic;

			exSwitchMovement_moveLeft_logic.IAmHere ();

			boolValues [0] = false;
			boolValues [1] = false;
		}

		public void LogicNodeUpdate ()
		{
			boolValues [0] = exSwitchMovement_moveLeft_logic.exSwitchMovement_moveLeft_logic_tagComparison.boolValue;

			GoToState ();
		}

		void GoToState ()
		{
			if ( ! boolValues [0])
			{
				return; 
			}

			exSwitchMovement_moveLeft_logic.exSwitchMovement_moveLeft.TomoveRight ();

		}

	}
}
