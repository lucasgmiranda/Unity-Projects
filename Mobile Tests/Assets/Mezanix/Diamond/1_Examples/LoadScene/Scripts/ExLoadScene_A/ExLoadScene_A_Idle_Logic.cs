using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExLoadScene_A_Idle_Logic 
	{
		public ExLoadScene_A_Idle_Logic_Button exLoadScene_A_Idle_Logic_Button;
		public ExLoadScene_A_Idle_Logic_Load exLoadScene_A_Idle_Logic_Load;
		
		public ExLoadScene_A_Idle exLoadScene_A_Idle;

		public ExLoadScene_A_Idle_Logic (ExLoadScene_A_Idle setExLoadScene_A_Idle)
		{
			exLoadScene_A_Idle = setExLoadScene_A_Idle;

			exLoadScene_A_Idle_Logic_Button = new ExLoadScene_A_Idle_Logic_Button (this);
			exLoadScene_A_Idle_Logic_Load = new ExLoadScene_A_Idle_Logic_Load (this);
		}

		public void LogicUpdate ()
		{
			exLoadScene_A_Idle_Logic_Button.LogicNodeUpdate ();
			exLoadScene_A_Idle_Logic_Load.LogicNodeUpdate ();
		}

		public void IAmHere ()
		{
		}	}
}
