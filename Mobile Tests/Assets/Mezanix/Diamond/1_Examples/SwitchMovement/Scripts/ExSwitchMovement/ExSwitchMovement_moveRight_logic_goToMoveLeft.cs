using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExSwitchMovement_moveRight_logic_goToMoveLeft 
	{
		ExSwitchMovement_moveRight_logic exSwitchMovement_moveRight_logic;

		public bool [] boolValues = new bool[2];

		public ExSwitchMovement_moveRight_logic_goToMoveLeft (ExSwitchMovement_moveRight_logic setExSwitchMovement_moveRight_logic) 
		{
			exSwitchMovement_moveRight_logic = setExSwitchMovement_moveRight_logic;

			exSwitchMovement_moveRight_logic.IAmHere ();

			boolValues [0] = false;
			boolValues [1] = false;
		}

		public void LogicNodeUpdate ()
		{
			boolValues [0] = exSwitchMovement_moveRight_logic.exSwitchMovement_moveRight_logic_tagComparison.boolValue;

			GoToState ();
		}

		void GoToState ()
		{
			if ( ! boolValues [0])
			{
				return; 
			}

			exSwitchMovement_moveRight_logic.exSwitchMovement_moveRight.TomoveLeft ();

		}

	}
}
