using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExSwitchMovement_Idle : IExSwitchMovement 
	{
		public ExSwitchMovement_Idle_logic exSwitchMovement_Idle_logic;
		
		public ExSwitchMovement exSwitchMovement; 

		public ExSwitchMovement_Idle (ExSwitchMovement setExSwitchMovement) 
		{
			exSwitchMovement = setExSwitchMovement;

			exSwitchMovement_Idle_logic = new ExSwitchMovement_Idle_logic (this);
		}

		public void StateUpdate () 
		{
			exSwitchMovement_Idle_logic.LogicUpdate ();
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
