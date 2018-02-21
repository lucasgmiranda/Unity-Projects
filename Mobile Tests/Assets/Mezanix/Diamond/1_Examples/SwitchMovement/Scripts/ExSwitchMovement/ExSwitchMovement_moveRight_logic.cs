using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExSwitchMovement_moveRight_logic 
	{
		public ExSwitchMovement_moveRight_logic_move exSwitchMovement_moveRight_logic_move;
		public ExSwitchMovement_moveRight_logic_RayForward exSwitchMovement_moveRight_logic_RayForward;
		public ExSwitchMovement_moveRight_logic_tagComparison exSwitchMovement_moveRight_logic_tagComparison;
		public ExSwitchMovement_moveRight_logic_Ui exSwitchMovement_moveRight_logic_Ui;
		public ExSwitchMovement_moveRight_logic_UiColor exSwitchMovement_moveRight_logic_UiColor;
		public ExSwitchMovement_moveRight_logic_goToMoveLeft exSwitchMovement_moveRight_logic_goToMoveLeft;
		
		public ExSwitchMovement_moveRight exSwitchMovement_moveRight;

		public ExSwitchMovement_moveRight_logic (ExSwitchMovement_moveRight setExSwitchMovement_moveRight)
		{
			exSwitchMovement_moveRight = setExSwitchMovement_moveRight;

			exSwitchMovement_moveRight_logic_move = new ExSwitchMovement_moveRight_logic_move (this);
			exSwitchMovement_moveRight_logic_RayForward = new ExSwitchMovement_moveRight_logic_RayForward (this);
			exSwitchMovement_moveRight_logic_tagComparison = new ExSwitchMovement_moveRight_logic_tagComparison (this);
			exSwitchMovement_moveRight_logic_Ui = new ExSwitchMovement_moveRight_logic_Ui (this);
			exSwitchMovement_moveRight_logic_UiColor = new ExSwitchMovement_moveRight_logic_UiColor (this);
			exSwitchMovement_moveRight_logic_goToMoveLeft = new ExSwitchMovement_moveRight_logic_goToMoveLeft (this);
		}

		public void LogicUpdate ()
		{
			exSwitchMovement_moveRight_logic_move.LogicNodeUpdate ();
			exSwitchMovement_moveRight_logic_RayForward.LogicNodeUpdate ();
			exSwitchMovement_moveRight_logic_tagComparison.LogicNodeUpdate ();
			exSwitchMovement_moveRight_logic_Ui.LogicNodeUpdate ();
			exSwitchMovement_moveRight_logic_UiColor.LogicNodeUpdate ();
			exSwitchMovement_moveRight_logic_goToMoveLeft.LogicNodeUpdate ();
		}

		public void IAmHere ()
		{
		}	}
}
