using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExPathFinder_1_Idle_Logic 
	{
		public ExPathFinder_1_Idle_Logic_RemainingDistance exPathFinder_1_Idle_Logic_RemainingDistance;
		public ExPathFinder_1_Idle_Logic_TargetFound exPathFinder_1_Idle_Logic_TargetFound;
		public ExPathFinder_1_Idle_Logic_Increment exPathFinder_1_Idle_Logic_Increment;
		public ExPathFinder_1_Idle_Logic_DivideInt exPathFinder_1_Idle_Logic_DivideInt;
		public ExPathFinder_1_Idle_Logic_TargetsList exPathFinder_1_Idle_Logic_TargetsList;
		public ExPathFinder_1_Idle_Logic_Target exPathFinder_1_Idle_Logic_Target;
		public ExPathFinder_1_Idle_Logic_GoToTarget exPathFinder_1_Idle_Logic_GoToTarget;
		public ExPathFinder_1_Idle_Logic_Start exPathFinder_1_Idle_Logic_Start;
		public ExPathFinder_1_Idle_Logic_Or exPathFinder_1_Idle_Logic_Or;
		
		public ExPathFinder_1_Idle exPathFinder_1_Idle;

		public ExPathFinder_1_Idle_Logic (ExPathFinder_1_Idle setExPathFinder_1_Idle)
		{
			exPathFinder_1_Idle = setExPathFinder_1_Idle;

			exPathFinder_1_Idle_Logic_RemainingDistance = new ExPathFinder_1_Idle_Logic_RemainingDistance (this);
			exPathFinder_1_Idle_Logic_TargetFound = new ExPathFinder_1_Idle_Logic_TargetFound (this);
			exPathFinder_1_Idle_Logic_Increment = new ExPathFinder_1_Idle_Logic_Increment (this);
			exPathFinder_1_Idle_Logic_DivideInt = new ExPathFinder_1_Idle_Logic_DivideInt (this);
			exPathFinder_1_Idle_Logic_TargetsList = new ExPathFinder_1_Idle_Logic_TargetsList (this);
			exPathFinder_1_Idle_Logic_Target = new ExPathFinder_1_Idle_Logic_Target (this);
			exPathFinder_1_Idle_Logic_GoToTarget = new ExPathFinder_1_Idle_Logic_GoToTarget (this);
			exPathFinder_1_Idle_Logic_Start = new ExPathFinder_1_Idle_Logic_Start (this);
			exPathFinder_1_Idle_Logic_Or = new ExPathFinder_1_Idle_Logic_Or (this);
		}

		public void LogicUpdate ()
		{
			exPathFinder_1_Idle_Logic_RemainingDistance.LogicNodeUpdate ();
			exPathFinder_1_Idle_Logic_TargetFound.LogicNodeUpdate ();
			exPathFinder_1_Idle_Logic_Increment.LogicNodeUpdate ();
			exPathFinder_1_Idle_Logic_DivideInt.LogicNodeUpdate ();
			exPathFinder_1_Idle_Logic_TargetsList.LogicNodeUpdate ();
			exPathFinder_1_Idle_Logic_Target.LogicNodeUpdate ();
			exPathFinder_1_Idle_Logic_GoToTarget.LogicNodeUpdate ();
			exPathFinder_1_Idle_Logic_Start.LogicNodeUpdate ();
			exPathFinder_1_Idle_Logic_Or.LogicNodeUpdate ();
		}

		public void IAmHere ()
		{
		}	}
}
