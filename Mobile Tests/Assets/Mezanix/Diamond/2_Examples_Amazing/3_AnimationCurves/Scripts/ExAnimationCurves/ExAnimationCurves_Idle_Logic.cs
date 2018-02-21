using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExAnimationCurves_Idle_Logic 
	{
		public ExAnimationCurves_Idle_Logic_A_key exAnimationCurves_Idle_Logic_A_key;
		public ExAnimationCurves_Idle_Logic_AnimationDuration exAnimationCurves_Idle_Logic_AnimationDuration;
		public ExAnimationCurves_Idle_Logic_AnimateTransformScale exAnimationCurves_Idle_Logic_AnimateTransformScale;
		public ExAnimationCurves_Idle_Logic_AnimateTransformPosition exAnimationCurves_Idle_Logic_AnimateTransformPosition;
		public ExAnimationCurves_Idle_Logic_AnimateColor exAnimationCurves_Idle_Logic_AnimateColor;
		public ExAnimationCurves_Idle_Logic_AnimateTransformRotation exAnimationCurves_Idle_Logic_AnimateTransformRotation;
		public ExAnimationCurves_Idle_Logic_SetMaterialColor exAnimationCurves_Idle_Logic_SetMaterialColor;
		
		public ExAnimationCurves_Idle exAnimationCurves_Idle;

		public ExAnimationCurves_Idle_Logic (ExAnimationCurves_Idle setExAnimationCurves_Idle)
		{
			exAnimationCurves_Idle = setExAnimationCurves_Idle;

			exAnimationCurves_Idle_Logic_A_key = new ExAnimationCurves_Idle_Logic_A_key (this);
			exAnimationCurves_Idle_Logic_AnimationDuration = new ExAnimationCurves_Idle_Logic_AnimationDuration (this);
			exAnimationCurves_Idle_Logic_AnimateTransformScale = new ExAnimationCurves_Idle_Logic_AnimateTransformScale (this);
			exAnimationCurves_Idle_Logic_AnimateTransformPosition = new ExAnimationCurves_Idle_Logic_AnimateTransformPosition (this);
			exAnimationCurves_Idle_Logic_AnimateColor = new ExAnimationCurves_Idle_Logic_AnimateColor (this);
			exAnimationCurves_Idle_Logic_AnimateTransformRotation = new ExAnimationCurves_Idle_Logic_AnimateTransformRotation (this);
			exAnimationCurves_Idle_Logic_SetMaterialColor = new ExAnimationCurves_Idle_Logic_SetMaterialColor (this);
		}

		public void LogicUpdate ()
		{
			exAnimationCurves_Idle_Logic_A_key.LogicNodeUpdate ();
			exAnimationCurves_Idle_Logic_AnimationDuration.LogicNodeUpdate ();
			exAnimationCurves_Idle_Logic_AnimateTransformScale.LogicNodeUpdate ();
			exAnimationCurves_Idle_Logic_AnimateTransformPosition.LogicNodeUpdate ();
			exAnimationCurves_Idle_Logic_AnimateColor.LogicNodeUpdate ();
			exAnimationCurves_Idle_Logic_AnimateTransformRotation.LogicNodeUpdate ();
			exAnimationCurves_Idle_Logic_SetMaterialColor.LogicNodeUpdate ();
		}

		public void IAmHere ()
		{
		}	}
}
