using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExDataTransferListener_Idle : IExDataTransferListener 
	{
		public ExDataTransferListener_Idle_Logic exDataTransferListener_Idle_Logic;
		
		public ExDataTransferListener exDataTransferListener; 

		public ExDataTransferListener_Idle (ExDataTransferListener setExDataTransferListener) 
		{
			exDataTransferListener = setExDataTransferListener;

			exDataTransferListener_Idle_Logic = new ExDataTransferListener_Idle_Logic (this);
		}

		public void StateUpdate () 
		{
			exDataTransferListener_Idle_Logic.LogicUpdate ();
		}

		public void ToIdle ()
		{
			exDataTransferListener.currentState = exDataTransferListener.idle;

		}

	}
}
