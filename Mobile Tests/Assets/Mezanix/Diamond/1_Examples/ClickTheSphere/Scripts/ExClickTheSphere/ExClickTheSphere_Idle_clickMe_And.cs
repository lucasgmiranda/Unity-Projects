using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExClickTheSphere_Idle_clickMe_And 
	{
		ExClickTheSphere_Idle_clickMe exClickTheSphere_Idle_clickMe;

		public bool boolValue = false;

		public bool [] boolValues = new bool[2];

		public ExClickTheSphere_Idle_clickMe_And (ExClickTheSphere_Idle_clickMe setExClickTheSphere_Idle_clickMe) 
		{
			exClickTheSphere_Idle_clickMe = setExClickTheSphere_Idle_clickMe;

			exClickTheSphere_Idle_clickMe.IAmHere ();

			boolValues [0] = false;
			boolValues [1] = false;
		}

		public void LogicNodeUpdate ()
		{
			boolValues [0] = exClickTheSphere_Idle_clickMe.exClickTheSphere_Idle_clickMe_Ray_.boolValue;

			boolValues [1] = exClickTheSphere_Idle_clickMe.exClickTheSphere_Idle_clickMe_Tag.boolValue;

			boolValue = (boolValues [0] && boolValues [1]);
		}

	}
}
