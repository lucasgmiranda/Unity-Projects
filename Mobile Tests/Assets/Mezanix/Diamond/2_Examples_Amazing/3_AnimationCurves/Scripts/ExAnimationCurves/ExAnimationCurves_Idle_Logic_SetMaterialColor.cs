using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExAnimationCurves_Idle_Logic_SetMaterialColor 
	{
		ExAnimationCurves_Idle_Logic exAnimationCurves_Idle_Logic;

		public bool doIT = false;

		ScriptsCreatedByDiamond.IdentifiedObjects identifiedObjects = null;

		public Material materialValue;


		public Material[] materialValues = new Material[2];

		public Color[] colorValues = new Color[2];

		public ExAnimationCurves_Idle_Logic_SetMaterialColor (ExAnimationCurves_Idle_Logic setExAnimationCurves_Idle_Logic) 
		{
			exAnimationCurves_Idle_Logic = setExAnimationCurves_Idle_Logic;

			exAnimationCurves_Idle_Logic.IAmHere ();

			identifiedObjects = GameObject.Find (ScriptsCreatedByDiamond.IdentifiedObjectsActions.gameObjectHolderName).GetComponent<ScriptsCreatedByDiamond.IdentifiedObjects>();

			if (identifiedObjects != null)
			{
				materialValues [0] = (Material)identifiedObjects.GetIdentifiedObject ("8_15_2017_11_03_02_AM_6353.materialValues_0");
			}

			colorValues [0] = new Color (1f, 1f, 1f, 0f);
			colorValues [1] = new Color (1f, 1f, 1f, 1f);

		}

		public void LogicNodeUpdate ()
		{
			colorValues [0] = exAnimationCurves_Idle_Logic.exAnimationCurves_Idle_Logic_AnimateColor.colorValue;

			doIT = true;

			ComputeMaterial ();
		}

		void ComputeMaterial ()
		{
			if ( ! doIT)
			{
				return;
			}

			if (materialValues [0] != null)
			{
				materialValues [0].color = colorValues [0];

				materialValue = materialValues [0];
			}

		}
	}
}
