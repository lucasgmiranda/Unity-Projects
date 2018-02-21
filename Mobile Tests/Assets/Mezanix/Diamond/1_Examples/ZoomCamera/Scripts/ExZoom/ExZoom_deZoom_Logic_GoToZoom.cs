using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExZoom_deZoom_Logic_GoToZoom 
	{
		ExZoom_deZoom_Logic exZoom_deZoom_Logic;

		public bool [] boolValues = new bool[2];

		public ExZoom_deZoom_Logic_GoToZoom (ExZoom_deZoom_Logic setExZoom_deZoom_Logic) 
		{
			exZoom_deZoom_Logic = setExZoom_deZoom_Logic;

			exZoom_deZoom_Logic.IAmHere ();

			boolValues [0] = false;
			boolValues [1] = false;
		}

		public void LogicNodeUpdate ()
		{
			boolValues [0] = exZoom_deZoom_Logic.exZoom_deZoom_Logic_RightClick.boolValue;

			GoToState ();
		}

		void GoToState ()
		{
			if ( ! boolValues [0])
			{
				return; 
			}

			exZoom_deZoom_Logic.exZoom_deZoom.Tozoom ();

		}

	}
}
