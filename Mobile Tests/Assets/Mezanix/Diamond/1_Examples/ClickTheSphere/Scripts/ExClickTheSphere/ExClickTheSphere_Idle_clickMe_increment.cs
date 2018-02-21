using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExClickTheSphere_Idle_clickMe_increment 
	{
		ExClickTheSphere_Idle_clickMe exClickTheSphere_Idle_clickMe;

		public bool doIT = false;

		public int intValue = 0;

		public int [] intValues = new int[3];

		static int intStatic;
		public ExClickTheSphere_Idle_clickMe_increment (ExClickTheSphere_Idle_clickMe setExClickTheSphere_Idle_clickMe) 
		{
			exClickTheSphere_Idle_clickMe = setExClickTheSphere_Idle_clickMe;

			exClickTheSphere_Idle_clickMe.IAmHere ();

			intValues [0] = 1;
			intValues [1] = 0;
			intValues [2] = 0;

		}

		public void LogicNodeUpdate ()
		{
			doIT = exClickTheSphere_Idle_clickMe.exClickTheSphere_Idle_clickMe_And.boolValue;

			ComputeInt ();
		}

		void ComputeInt ()
		{
			if ( ! doIT)
			{
				return;
			}


			intStatic += intValues [0];
			intValue = intStatic;

		}
	}
}
