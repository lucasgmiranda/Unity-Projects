using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExZoom_zoom_Logic_copy 
	{
		public ExZoom_zoom_Logic_copy_setFieldOfView exZoom_zoom_Logic_copy_setFieldOfView;
		public ExZoom_zoom_Logic_copy_RightClick exZoom_zoom_Logic_copy_RightClick;
		public ExZoom_zoom_Logic_copy_GoToZoom exZoom_zoom_Logic_copy_GoToZoom;
		
		public ExZoom_zoom exZoom_zoom;

		public ExZoom_zoom_Logic_copy (ExZoom_zoom setExZoom_zoom)
		{
			exZoom_zoom = setExZoom_zoom;

			exZoom_zoom_Logic_copy_setFieldOfView = new ExZoom_zoom_Logic_copy_setFieldOfView (this);
			exZoom_zoom_Logic_copy_RightClick = new ExZoom_zoom_Logic_copy_RightClick (this);
			exZoom_zoom_Logic_copy_GoToZoom = new ExZoom_zoom_Logic_copy_GoToZoom (this);
		}

		public void LogicUpdate ()
		{
			exZoom_zoom_Logic_copy_setFieldOfView.LogicNodeUpdate ();
			exZoom_zoom_Logic_copy_RightClick.LogicNodeUpdate ();
			exZoom_zoom_Logic_copy_GoToZoom.LogicNodeUpdate ();
		}

		public void IAmHere ()
		{
		}	}
}
