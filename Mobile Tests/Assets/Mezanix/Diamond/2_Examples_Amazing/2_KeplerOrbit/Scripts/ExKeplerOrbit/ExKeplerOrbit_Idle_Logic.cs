using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExKeplerOrbit_Idle_Logic 
	{
		public ExKeplerOrbit_Idle_Logic_Ellipse exKeplerOrbit_Idle_Logic_Ellipse;
		
		public ExKeplerOrbit_Idle exKeplerOrbit_Idle;

		public ExKeplerOrbit_Idle_Logic (ExKeplerOrbit_Idle setExKeplerOrbit_Idle)
		{
			exKeplerOrbit_Idle = setExKeplerOrbit_Idle;

			exKeplerOrbit_Idle_Logic_Ellipse = new ExKeplerOrbit_Idle_Logic_Ellipse (this);
		}

		public void LogicUpdate ()
		{
			exKeplerOrbit_Idle_Logic_Ellipse.LogicNodeUpdate ();
		}

		public void IAmHere ()
		{
		}	}
}
