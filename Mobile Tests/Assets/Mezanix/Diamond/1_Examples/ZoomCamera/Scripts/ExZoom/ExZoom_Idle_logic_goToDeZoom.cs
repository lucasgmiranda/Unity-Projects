using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExZoom_Idle_logic_goToDeZoom 
	{
		ExZoom_Idle_logic exZoom_Idle_logic;

		public bool [] boolValues = new bool[2];

		public ExZoom_Idle_logic_goToDeZoom (ExZoom_Idle_logic setExZoom_Idle_logic) 
		{
			exZoom_Idle_logic = setExZoom_Idle_logic;

			exZoom_Idle_logic.IAmHere ();

			boolValues [0] = true;
			boolValues [1] = false;
		}

		public void LogicNodeUpdate ()
		{
			GoToState ();
		}

		void GoToState ()
		{
			if ( ! boolValues [0])
			{
				return; 
			}

			exZoom_Idle_logic.exZoom_Idle.TodeZoom ();

		}

	}
}
