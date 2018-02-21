using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExZoom_Idle_logic 
	{
		public ExZoom_Idle_logic_goToDeZoom exZoom_Idle_logic_goToDeZoom;
		
		public ExZoom_Idle exZoom_Idle;

		public ExZoom_Idle_logic (ExZoom_Idle setExZoom_Idle)
		{
			exZoom_Idle = setExZoom_Idle;

			exZoom_Idle_logic_goToDeZoom = new ExZoom_Idle_logic_goToDeZoom (this);
		}

		public void LogicUpdate ()
		{
			exZoom_Idle_logic_goToDeZoom.LogicNodeUpdate ();
		}

		public void IAmHere ()
		{
		}	}
}
