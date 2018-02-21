using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExSwitchMovement_moveLeft : IExSwitchMovement 
	{
		public ExSwitchMovement_moveLeft_logic exSwitchMovement_moveLeft_logic;
		
		public ExSwitchMovement exSwitchMovement; 

		public ExSwitchMovement_moveLeft (ExSwitchMovement setExSwitchMovement) 
		{
			exSwitchMovement = setExSwitchMovement;

			exSwitchMovement_moveLeft_logic = new ExSwitchMovement_moveLeft_logic (this);
		}

		public void StateUpdate () 
		{
			exSwitchMovement_moveLeft_logic.LogicUpdate ();
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
