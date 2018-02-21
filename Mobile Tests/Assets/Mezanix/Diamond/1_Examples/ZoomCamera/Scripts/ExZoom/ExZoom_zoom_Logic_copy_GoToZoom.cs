using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExZoom_zoom_Logic_copy_GoToZoom 
	{
		ExZoom_zoom_Logic_copy exZoom_zoom_Logic_copy;

		public bool [] boolValues = new bool[2];

		public ExZoom_zoom_Logic_copy_GoToZoom (ExZoom_zoom_Logic_copy setExZoom_zoom_Logic_copy) 
		{
			exZoom_zoom_Logic_copy = setExZoom_zoom_Logic_copy;

			exZoom_zoom_Logic_copy.IAmHere ();

			boolValues [0] = false;
			boolValues [1] = false;
		}

		public void LogicNodeUpdate ()
		{
			boolValues [0] = exZoom_zoom_Logic_copy.exZoom_zoom_Logic_copy_RightClick.boolValue;

			GoToState ();
		}

		void GoToState ()
		{
			if ( ! boolValues [0])
			{
				return; 
			}

			exZoom_zoom_Logic_copy.exZoom_zoom.TodeZoom ();

		}

	}
}
