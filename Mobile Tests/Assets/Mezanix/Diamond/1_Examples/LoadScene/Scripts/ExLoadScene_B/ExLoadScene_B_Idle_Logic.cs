using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExLoadScene_B_Idle_Logic 
	{
		public ExLoadScene_B_Idle_Logic_Button exLoadScene_B_Idle_Logic_Button;
		public ExLoadScene_B_Idle_Logic_Load exLoadScene_B_Idle_Logic_Load;
		
		public ExLoadScene_B_Idle exLoadScene_B_Idle;

		public ExLoadScene_B_Idle_Logic (ExLoadScene_B_Idle setExLoadScene_B_Idle)
		{
			exLoadScene_B_Idle = setExLoadScene_B_Idle;

			exLoadScene_B_Idle_Logic_Button = new ExLoadScene_B_Idle_Logic_Button (this);
			exLoadScene_B_Idle_Logic_Load = new ExLoadScene_B_Idle_Logic_Load (this);
		}

		public void LogicUpdate ()
		{
			exLoadScene_B_Idle_Logic_Button.LogicNodeUpdate ();
			exLoadScene_B_Idle_Logic_Load.LogicNodeUpdate ();
		}

		public void IAmHere ()
		{
		}	}
}
