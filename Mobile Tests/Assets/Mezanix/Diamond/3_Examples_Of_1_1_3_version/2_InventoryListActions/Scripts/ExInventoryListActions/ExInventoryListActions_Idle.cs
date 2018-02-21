using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExInventoryListActions_Idle : IExInventoryListActions 
	{
		public ExInventoryListActions_Idle_Logic exInventoryListActions_Idle_Logic;
		
		public ExInventoryListActions exInventoryListActions; 

		public ExInventoryListActions_Idle (ExInventoryListActions setExInventoryListActions) 
		{
			exInventoryListActions = setExInventoryListActions;

			exInventoryListActions_Idle_Logic = new ExInventoryListActions_Idle_Logic (this);
		}

		public void StateUpdate () 
		{
			exInventoryListActions_Idle_Logic.LogicUpdate ();
		}

		public void ToIdle ()
		{
			exInventoryListActions.idle.exInventoryListActions_Idle_Logic.exInventoryListActions_Idle_Logic_9_10_2017_4_15_11_PM_6138.intValues [0] = 0;
			exInventoryListActions.idle.exInventoryListActions_Idle_Logic.exInventoryListActions_Idle_Logic_9_10_2017_4_15_11_PM_6138.boolValue = true;

			exInventoryListActions.currentState = exInventoryListActions.idle;

		}

	}
}
