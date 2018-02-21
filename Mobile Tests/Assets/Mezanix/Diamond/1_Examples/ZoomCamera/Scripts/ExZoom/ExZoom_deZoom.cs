using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExZoom_deZoom : IExZoom 
	{
		public ExZoom_deZoom_Logic exZoom_deZoom_Logic;
		
		public ExZoom exZoom; 

		public ExZoom_deZoom (ExZoom setExZoom) 
		{
			exZoom = setExZoom;

			exZoom_deZoom_Logic = new ExZoom_deZoom_Logic (this);
		}

		public void StateUpdate () 
		{
			exZoom_deZoom_Logic.LogicUpdate ();
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
