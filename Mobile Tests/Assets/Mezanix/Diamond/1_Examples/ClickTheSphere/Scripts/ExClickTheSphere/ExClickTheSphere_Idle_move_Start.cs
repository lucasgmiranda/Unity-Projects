using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExClickTheSphere_Idle_move_Start 
	{
		ExClickTheSphere_Idle_move exClickTheSphere_Idle_move;

		public bool boolValue = false;

		public int [] intValues = new int[3];

		public ExClickTheSphere_Idle_move_Start (ExClickTheSphere_Idle_move setExClickTheSphere_Idle_move) 
		{
			exClickTheSphere_Idle_move = setExClickTheSphere_Idle_move;

			exClickTheSphere_Idle_move.IAmHere ();

			boolValue = true;

			intValues [0] = 0;
			intValues [1] = 1;
			intValues [2] = 0;

			intValues [0] = 0;
		}

		public void LogicNodeUpdate ()
		{
			intValues [0]++;

			if (intValues [0] > intValues [1])
			{
				boolValue = false;
			}
		}

	}
}
