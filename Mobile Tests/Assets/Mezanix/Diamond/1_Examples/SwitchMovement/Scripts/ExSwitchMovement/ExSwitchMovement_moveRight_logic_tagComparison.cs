using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExSwitchMovement_moveRight_logic_tagComparison 
	{
		ExSwitchMovement_moveRight_logic exSwitchMovement_moveRight_logic;

		public bool doIT = false;

		public string [] stringValues = new string[2];

		public bool boolValue = false;

		public ExSwitchMovement_moveRight_logic_tagComparison (ExSwitchMovement_moveRight_logic setExSwitchMovement_moveRight_logic) 
		{
			exSwitchMovement_moveRight_logic = setExSwitchMovement_moveRight_logic;

			exSwitchMovement_moveRight_logic.IAmHere ();

			stringValues [0] = "right";
			stringValues [1] = "";
		}

		public void LogicNodeUpdate ()
		{
			stringValues [1] = exSwitchMovement_moveRight_logic.exSwitchMovement_moveRight_logic_RayForward.stringValue;

			doIT = true;

			ComputeString ();
		}

		void ComputeString ()
		{
			if ( ! doIT)
			{
				boolValue = false;
				return;
			}


			boolValue = (stringValues [0] == stringValues [1])?true: false;

		}
	}
}
