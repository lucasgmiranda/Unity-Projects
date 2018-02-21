using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExZoom_deZoom_Logic 
	{
		public ExZoom_deZoom_Logic_setFieldOfView exZoom_deZoom_Logic_setFieldOfView;
		public ExZoom_deZoom_Logic_RightClick exZoom_deZoom_Logic_RightClick;
		public ExZoom_deZoom_Logic_GoToZoom exZoom_deZoom_Logic_GoToZoom;
		
		public ExZoom_deZoom exZoom_deZoom;

		public ExZoom_deZoom_Logic (ExZoom_deZoom setExZoom_deZoom)
		{
			exZoom_deZoom = setExZoom_deZoom;

			exZoom_deZoom_Logic_setFieldOfView = new ExZoom_deZoom_Logic_setFieldOfView (this);
			exZoom_deZoom_Logic_RightClick = new ExZoom_deZoom_Logic_RightClick (this);
			exZoom_deZoom_Logic_GoToZoom = new ExZoom_deZoom_Logic_GoToZoom (this);
		}

		public void LogicUpdate ()
		{
			exZoom_deZoom_Logic_setFieldOfView.LogicNodeUpdate ();
			exZoom_deZoom_Logic_RightClick.LogicNodeUpdate ();
			exZoom_deZoom_Logic_GoToZoom.LogicNodeUpdate ();
		}

		public void IAmHere ()
		{
		}	}
}
