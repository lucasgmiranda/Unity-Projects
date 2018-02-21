using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExZoom_zoom : IExZoom 
	{
		public ExZoom_zoom_Logic_copy exZoom_zoom_Logic_copy;
		
		public ExZoom exZoom; 

		public ExZoom_zoom (ExZoom setExZoom) 
		{
			exZoom = setExZoom;

			exZoom_zoom_Logic_copy = new ExZoom_zoom_Logic_copy (this);
		}

		public void StateUpdate () 
		{
			exZoom_zoom_Logic_copy.LogicUpdate ();
		}

		public void ToIdle ()
		{
			exZoom.currentState = exZoom.idle;
		}

		public void TodeZoom ()
		{
			exZoom.currentState = exZoom.deZoom;
		}

		public void Tozoom ()
		{
			exZoom.currentState = exZoom.zoom;
		}

	}
}
