using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExDataTransferListener_Idle_Logic 
	{
		public ExDataTransferListener_Idle_Logic_9_10_2017_12_15_59_PM_6773 exDataTransferListener_Idle_Logic_9_10_2017_12_15_59_PM_6773;
		public ExDataTransferListener_Idle_Logic_9_10_2017_12_16_20_PM_6657 exDataTransferListener_Idle_Logic_9_10_2017_12_16_20_PM_6657;
		public ExDataTransferListener_Idle_Logic_9_10_2017_12_17_08_PM_5612 exDataTransferListener_Idle_Logic_9_10_2017_12_17_08_PM_5612;
		public ExDataTransferListener_Idle_Logic_9_10_2017_12_16_46_PM_0571 exDataTransferListener_Idle_Logic_9_10_2017_12_16_46_PM_0571;
		
		public ExDataTransferListener_Idle exDataTransferListener_Idle;

		public ExDataTransferListener_Idle_Logic (ExDataTransferListener_Idle setExDataTransferListener_Idle)
		{
			exDataTransferListener_Idle = setExDataTransferListener_Idle;

			exDataTransferListener_Idle_Logic_9_10_2017_12_15_59_PM_6773 = new ExDataTransferListener_Idle_Logic_9_10_2017_12_15_59_PM_6773 (this);
			exDataTransferListener_Idle_Logic_9_10_2017_12_16_20_PM_6657 = new ExDataTransferListener_Idle_Logic_9_10_2017_12_16_20_PM_6657 (this);
			exDataTransferListener_Idle_Logic_9_10_2017_12_17_08_PM_5612 = new ExDataTransferListener_Idle_Logic_9_10_2017_12_17_08_PM_5612 (this);
			exDataTransferListener_Idle_Logic_9_10_2017_12_16_46_PM_0571 = new ExDataTransferListener_Idle_Logic_9_10_2017_12_16_46_PM_0571 (this);
		}

		public void LogicUpdate ()
		{
			exDataTransferListener_Idle_Logic_9_10_2017_12_15_59_PM_6773.LogicNodeUpdate ();
			exDataTransferListener_Idle_Logic_9_10_2017_12_16_20_PM_6657.LogicNodeUpdate ();
			exDataTransferListener_Idle_Logic_9_10_2017_12_17_08_PM_5612.LogicNodeUpdate ();
			exDataTransferListener_Idle_Logic_9_10_2017_12_16_46_PM_0571.LogicNodeUpdate ();
		}

		public void IAmHere ()
		{
		}
	}
}
