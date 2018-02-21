using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExDataTransferSender_Idle_Logic 
	{
		public ExDataTransferSender_Idle_Logic_9_10_2017_12_13_25_PM_4364 exDataTransferSender_Idle_Logic_9_10_2017_12_13_25_PM_4364;
		public ExDataTransferSender_Idle_Logic_9_10_2017_12_13_56_PM_7413 exDataTransferSender_Idle_Logic_9_10_2017_12_13_56_PM_7413;
		
		public ExDataTransferSender_Idle exDataTransferSender_Idle;

		public ExDataTransferSender_Idle_Logic (ExDataTransferSender_Idle setExDataTransferSender_Idle)
		{
			exDataTransferSender_Idle = setExDataTransferSender_Idle;

			exDataTransferSender_Idle_Logic_9_10_2017_12_13_25_PM_4364 = new ExDataTransferSender_Idle_Logic_9_10_2017_12_13_25_PM_4364 (this);
			exDataTransferSender_Idle_Logic_9_10_2017_12_13_56_PM_7413 = new ExDataTransferSender_Idle_Logic_9_10_2017_12_13_56_PM_7413 (this);
		}

		public void LogicUpdate ()
		{
			exDataTransferSender_Idle_Logic_9_10_2017_12_13_25_PM_4364.LogicNodeUpdate ();
			exDataTransferSender_Idle_Logic_9_10_2017_12_13_56_PM_7413.LogicNodeUpdate ();
		}

		public void IAmHere ()
		{
		}	}
}
