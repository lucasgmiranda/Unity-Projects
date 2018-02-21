using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExClickTheSphere_Idle_clickMe_toString 
	{
		ExClickTheSphere_Idle_clickMe exClickTheSphere_Idle_clickMe;

		public bool doIT = false;

		public int [] intValues = new int[3];

		public string stringValue;

		public ExClickTheSphere_Idle_clickMe_toString (ExClickTheSphere_Idle_clickMe setExClickTheSphere_Idle_clickMe) 
		{
			exClickTheSphere_Idle_clickMe = setExClickTheSphere_Idle_clickMe;

			exClickTheSphere_Idle_clickMe.IAmHere ();

			intValues [0] = 0;
			intValues [1] = 0;
			intValues [2] = 0;

		}

		public void LogicNodeUpdate ()
		{
			intValues [0] = exClickTheSphere_Idle_clickMe.exClickTheSphere_Idle_clickMe_increment.intValue;

			doIT = exClickTheSphere_Idle_clickMe.exClickTheSphere_Idle_clickMe_LeftClick.boolValue;

			ComputeInt ();
		}

		void ComputeInt ()
		{
			if ( ! doIT)
			{
				return;
			}


			stringValue = intValues [0].ToString ();

		}
	}
}
