using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExAtStateStart_Counter_Logic 
	{
		public ExAtStateStart_Counter_Logic_9_10_2017_4_37_47_PM_0516 exAtStateStart_Counter_Logic_9_10_2017_4_37_47_PM_0516;
		public ExAtStateStart_Counter_Logic_9_10_2017_4_38_26_PM_8580 exAtStateStart_Counter_Logic_9_10_2017_4_38_26_PM_8580;
		public ExAtStateStart_Counter_Logic_9_10_2017_4_36_10_PM_0725 exAtStateStart_Counter_Logic_9_10_2017_4_36_10_PM_0725;
		public ExAtStateStart_Counter_Logic_9_10_2017_4_36_26_PM_6665 exAtStateStart_Counter_Logic_9_10_2017_4_36_26_PM_6665;
		public ExAtStateStart_Counter_Logic_9_10_2017_4_36_58_PM_3513 exAtStateStart_Counter_Logic_9_10_2017_4_36_58_PM_3513;
		public ExAtStateStart_Counter_Logic_9_10_2017_4_37_16_PM_5421 exAtStateStart_Counter_Logic_9_10_2017_4_37_16_PM_5421;
		public ExAtStateStart_Counter_Logic_9_10_2017_4_38_06_PM_1356 exAtStateStart_Counter_Logic_9_10_2017_4_38_06_PM_1356;
		
		public ExAtStateStart_Counter exAtStateStart_Counter;

		public ExAtStateStart_Counter_Logic (ExAtStateStart_Counter setExAtStateStart_Counter)
		{
			exAtStateStart_Counter = setExAtStateStart_Counter;

			exAtStateStart_Counter_Logic_9_10_2017_4_37_47_PM_0516 = new ExAtStateStart_Counter_Logic_9_10_2017_4_37_47_PM_0516 (this);
			exAtStateStart_Counter_Logic_9_10_2017_4_38_26_PM_8580 = new ExAtStateStart_Counter_Logic_9_10_2017_4_38_26_PM_8580 (this);
			exAtStateStart_Counter_Logic_9_10_2017_4_36_10_PM_0725 = new ExAtStateStart_Counter_Logic_9_10_2017_4_36_10_PM_0725 (this);
			exAtStateStart_Counter_Logic_9_10_2017_4_36_26_PM_6665 = new ExAtStateStart_Counter_Logic_9_10_2017_4_36_26_PM_6665 (this);
			exAtStateStart_Counter_Logic_9_10_2017_4_36_58_PM_3513 = new ExAtStateStart_Counter_Logic_9_10_2017_4_36_58_PM_3513 (this);
			exAtStateStart_Counter_Logic_9_10_2017_4_37_16_PM_5421 = new ExAtStateStart_Counter_Logic_9_10_2017_4_37_16_PM_5421 (this);
			exAtStateStart_Counter_Logic_9_10_2017_4_38_06_PM_1356 = new ExAtStateStart_Counter_Logic_9_10_2017_4_38_06_PM_1356 (this);
		}

		public void LogicUpdate ()
		{
			exAtStateStart_Counter_Logic_9_10_2017_4_37_47_PM_0516.LogicNodeUpdate ();
			exAtStateStart_Counter_Logic_9_10_2017_4_38_26_PM_8580.LogicNodeUpdate ();
			exAtStateStart_Counter_Logic_9_10_2017_4_36_10_PM_0725.LogicNodeUpdate ();
			exAtStateStart_Counter_Logic_9_10_2017_4_36_26_PM_6665.LogicNodeUpdate ();
			exAtStateStart_Counter_Logic_9_10_2017_4_36_58_PM_3513.LogicNodeUpdate ();
			exAtStateStart_Counter_Logic_9_10_2017_4_37_16_PM_5421.LogicNodeUpdate ();
			exAtStateStart_Counter_Logic_9_10_2017_4_38_06_PM_1356.LogicNodeUpdate ();
		}

		public void IAmHere ()
		{
		}	}
}
