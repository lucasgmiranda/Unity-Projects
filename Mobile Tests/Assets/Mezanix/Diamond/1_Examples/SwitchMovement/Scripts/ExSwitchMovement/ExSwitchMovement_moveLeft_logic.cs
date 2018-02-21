using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExSwitchMovement_moveLeft_logic 
	{
		public ExSwitchMovement_moveLeft_logic_move exSwitchMovement_moveLeft_logic_move;
		public ExSwitchMovement_moveLeft_logic_RayForward exSwitchMovement_moveLeft_logic_RayForward;
		public ExSwitchMovement_moveLeft_logic_tagComparison exSwitchMovement_moveLeft_logic_tagComparison;
		public ExSwitchMovement_moveLeft_logic_Ui exSwitchMovement_moveLeft_logic_Ui;
		public ExSwitchMovement_moveLeft_logic_UiColor exSwitchMovement_moveLeft_logic_UiColor;
		public ExSwitchMovement_moveLeft_logic_goToMoveLeft exSwitchMovement_moveLeft_logic_goToMoveLeft;
		
		public ExSwitchMovement_moveLeft exSwitchMovement_moveLeft;

		public ExSwitchMovement_moveLeft_logic (ExSwitchMovement_moveLeft setExSwitchMovement_moveLeft)
		{
			exSwitchMovement_moveLeft = setExSwitchMovement_moveLeft;

			exSwitchMovement_moveLeft_logic_move = new ExSwitchMovement_moveLeft_logic_move (this);
			exSwitchMovement_moveLeft_logic_RayForward = new ExSwitchMovement_moveLeft_logic_RayForward (this);
			exSwitchMovement_moveLeft_logic_tagComparison = new ExSwitchMovement_moveLeft_logic_tagComparison (this);
			exSwitchMovement_moveLeft_logic_Ui = new ExSwitchMovement_moveLeft_logic_Ui (this);
			exSwitchMovement_moveLeft_logic_UiColor = new ExSwitchMovement_moveLeft_logic_UiColor (this);
			exSwitchMovement_moveLeft_logic_goToMoveLeft = new ExSwitchMovement_moveLeft_logic_goToMoveLeft (this);
		}

		public void LogicUpdate ()
		{
			exSwitchMovement_moveLeft_logic_move.LogicNodeUpdate ();
			exSwitchMovement_moveLeft_logic_RayForward.LogicNodeUpdate ();
			exSwitchMovement_moveLeft_logic_tagComparison.LogicNodeUpdate ();
			exSwitchMovement_moveLeft_logic_Ui.LogicNodeUpdate ();
			exSwitchMovement_moveLeft_logic_UiColor.LogicNodeUpdate ();
			exSwitchMovement_moveLeft_logic_goToMoveLeft.LogicNodeUpdate ();
		}

		public void IAmHere ()
		{
		}	}
}
