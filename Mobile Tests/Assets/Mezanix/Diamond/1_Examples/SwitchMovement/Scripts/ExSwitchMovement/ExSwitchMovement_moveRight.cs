using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExSwitchMovement_moveRight : IExSwitchMovement 
	{
		public ExSwitchMovement_moveRight_logic exSwitchMovement_moveRight_logic;
		
		public ExSwitchMovement exSwitchMovement; 

		public ExSwitchMovement_moveRight (ExSwitchMovement setExSwitchMovement) 
		{
			exSwitchMovement = setExSwitchMovement;

			exSwitchMovement_moveRight_logic = new ExSwitchMovement_moveRight_logic (this);
		}

		public void StateUpdate () 
		{
			exSwitchMovement_moveRight_logic.LogicUpdate ();
		}

		public void ToIdle ()
		{
			exSwitchMovement.currentState = exSwitchMovement.idle;
		}

		public void TomoveRight ()
		{
			exSwitchMovement.currentState = exSwitchMovement.moveRight;
		}

		public void TomoveLeft ()
		{
			exSwitchMovement.currentState = exSwitchMovement.moveLeft;
		}

	}
}
