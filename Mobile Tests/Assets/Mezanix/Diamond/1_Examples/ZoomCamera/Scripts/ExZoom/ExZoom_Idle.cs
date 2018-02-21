using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExZoom_Idle : IExZoom 
	{
		public ExZoom_Idle_logic exZoom_Idle_logic;
		
		public ExZoom exZoom; 

		public ExZoom_Idle (ExZoom setExZoom) 
		{
			exZoom = setExZoom;

			exZoom_Idle_logic = new ExZoom_Idle_logic (this);
		}

		public void StateUpdate () 
		{
			exZoom_Idle_logic.LogicUpdate ();
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
