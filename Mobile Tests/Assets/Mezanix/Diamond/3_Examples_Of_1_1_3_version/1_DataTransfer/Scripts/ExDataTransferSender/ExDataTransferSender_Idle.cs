using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExDataTransferSender_Idle : IExDataTransferSender 
	{
		public ExDataTransferSender_Idle_Logic exDataTransferSender_Idle_Logic;
		
		public ExDataTransferSender exDataTransferSender; 

		public ExDataTransferSender_Idle (ExDataTransferSender setExDataTransferSender) 
		{
			exDataTransferSender = setExDataTransferSender;

			exDataTransferSender_Idle_Logic = new ExDataTransferSender_Idle_Logic (this);
		}

		public void StateUpdate () 
		{
			exDataTransferSender_Idle_Logic.LogicUpdate ();
		}

		public void ToIdle ()
		{
			exDataTransferSender.currentState = exDataTransferSender.idle;

		}

	}
}
