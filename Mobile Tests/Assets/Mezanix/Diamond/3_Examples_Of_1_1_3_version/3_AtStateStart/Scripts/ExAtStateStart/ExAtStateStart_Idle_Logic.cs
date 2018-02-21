using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExAtStateStart_Idle_Logic 
	{
		public ExAtStateStart_Idle_Logic_9_10_2017_4_40_51_PM_2806 exAtStateStart_Idle_Logic_9_10_2017_4_40_51_PM_2806;
		public ExAtStateStart_Idle_Logic_9_10_2017_4_40_55_PM_5815 exAtStateStart_Idle_Logic_9_10_2017_4_40_55_PM_5815;
		public ExAtStateStart_Idle_Logic_9_10_2017_4_41_20_PM_2875 exAtStateStart_Idle_Logic_9_10_2017_4_41_20_PM_2875;
		public ExAtStateStart_Idle_Logic_9_10_2017_4_41_35_PM_4211 exAtStateStart_Idle_Logic_9_10_2017_4_41_35_PM_4211;
		
		public ExAtStateStart_Idle exAtStateStart_Idle;

		public ExAtStateStart_Idle_Logic (ExAtStateStart_Idle setExAtStateStart_Idle)
		{
			exAtStateStart_Idle = setExAtStateStart_Idle;

			exAtStateStart_Idle_Logic_9_10_2017_4_40_51_PM_2806 = new ExAtStateStart_Idle_Logic_9_10_2017_4_40_51_PM_2806 (this);
			exAtStateStart_Idle_Logic_9_10_2017_4_40_55_PM_5815 = new ExAtStateStart_Idle_Logic_9_10_2017_4_40_55_PM_5815 (this);
			exAtStateStart_Idle_Logic_9_10_2017_4_41_20_PM_2875 = new ExAtStateStart_Idle_Logic_9_10_2017_4_41_20_PM_2875 (this);
			exAtStateStart_Idle_Logic_9_10_2017_4_41_35_PM_4211 = new ExAtStateStart_Idle_Logic_9_10_2017_4_41_35_PM_4211 (this);
		}

		public void LogicUpdate ()
		{
			exAtStateStart_Idle_Logic_9_10_2017_4_40_51_PM_2806.LogicNodeUpdate ();
			exAtStateStart_Idle_Logic_9_10_2017_4_40_55_PM_5815.LogicNodeUpdate ();
			exAtStateStart_Idle_Logic_9_10_2017_4_41_20_PM_2875.LogicNodeUpdate ();
			exAtStateStart_Idle_Logic_9_10_2017_4_41_35_PM_4211.LogicNodeUpdate ();
		}

		public void IAmHere ()
		{
		}	}
}
