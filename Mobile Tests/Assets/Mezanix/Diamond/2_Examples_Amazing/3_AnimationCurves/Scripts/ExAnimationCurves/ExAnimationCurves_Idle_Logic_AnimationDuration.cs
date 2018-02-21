using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExAnimationCurves_Idle_Logic_AnimationDuration 
	{
		ExAnimationCurves_Idle_Logic exAnimationCurves_Idle_Logic;

		public bool doIT = false;

		public float floatValue = 0f;

		public ExAnimationCurves_Idle_Logic_AnimationDuration (ExAnimationCurves_Idle_Logic setExAnimationCurves_Idle_Logic) 
		{
			exAnimationCurves_Idle_Logic = setExAnimationCurves_Idle_Logic;

			exAnimationCurves_Idle_Logic.IAmHere ();

			floatValue = 3f;
		}

		public void LogicNodeUpdate ()
		{
		}

	}
}
