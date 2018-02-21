using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExAtStateStart_Idle : IExAtStateStart 
	{
		public ExAtStateStart_Idle_Logic exAtStateStart_Idle_Logic;
		
		public ExAtStateStart exAtStateStart; 

		public ExAtStateStart_Idle (ExAtStateStart setExAtStateStart) 
		{
			exAtStateStart = setExAtStateStart;

			exAtStateStart_Idle_Logic = new ExAtStateStart_Idle_Logic (this);
		}

		public void StateUpdate () 
		{
			exAtStateStart_Idle_Logic.LogicUpdate ();
		}

		public void ToIdle ()
		{
			exAtStateStart.idle.exAtStateStart_Idle_Logic.exAtStateStart_Idle_Logic_9_10_2017_4_40_51_PM_2806.intValues [0] = 0;
			exAtStateStart.idle.exAtStateStart_Idle_Logic.exAtStateStart_Idle_Logic_9_10_2017_4_40_51_PM_2806.boolValue = true;

			exAtStateStart.currentState = exAtStateStart.idle;

		}

		public void ToCounter ()
		{
			exAtStateStart.counter.exAtStateStart_Counter_Logic.exAtStateStart_Counter_Logic_9_10_2017_4_36_10_PM_0725.intValues [0] = 0;
			exAtStateStart.counter.exAtStateStart_Counter_Logic.exAtStateStart_Counter_Logic_9_10_2017_4_36_10_PM_0725.boolValue = true;

			exAtStateStart.currentState = exAtStateStart.counter;

		}

	}
}
