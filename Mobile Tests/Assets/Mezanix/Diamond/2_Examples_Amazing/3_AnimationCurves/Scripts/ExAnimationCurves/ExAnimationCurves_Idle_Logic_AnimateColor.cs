using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExAnimationCurves_Idle_Logic_AnimateColor 
	{
		ExAnimationCurves_Idle_Logic exAnimationCurves_Idle_Logic;

		public bool doIT = false;

		public float [] floatValues = new float[3];

		AnimationCurve animationCurve_;
		public bool [] boolValues = new bool[2];

		public Color colorValue;


		public Color[] colorValues = new Color[2];

		public ExAnimationCurves_Idle_Logic_AnimateColor (ExAnimationCurves_Idle_Logic setExAnimationCurves_Idle_Logic) 
		{
			exAnimationCurves_Idle_Logic = setExAnimationCurves_Idle_Logic;

			exAnimationCurves_Idle_Logic.IAmHere ();

			floatValues [0] = 1f;
			floatValues [1] = 1f;
			floatValues [2] = 3f;

			boolValues [0] = false;
			animationCurve_ = new AnimationCurve (
				new Keyframe []
				{
					new Keyframe (0f, 0f, 0f, 0f),
					new Keyframe (1f, 1f, 0f, 0f)
				});

			colorValues [0] = new Color (1f, 1f, 1f, 1f);
			colorValues [1] = new Color (1f, 0f, 0f, 1f);

			colorValue = new Color (1f, 1f, 1f, 0f);
		}

		public void LogicNodeUpdate ()
		{
			floatValues [2] = exAnimationCurves_Idle_Logic.exAnimationCurves_Idle_Logic_AnimationDuration.floatValue;

			doIT = exAnimationCurves_Idle_Logic.exAnimationCurves_Idle_Logic_A_key.boolValue;

			ComputeColor ();
		}

		IEnumerator RunAnimationCurve_color ()
		{
			if ( ! boolValues [0])
			{
				boolValues [0] = true;

				float t = 0f;

				float rate = 1f / floatValues [2];

				while (t < 1f)
				{
					t += Time.deltaTime * rate;

					colorValue = Color.Lerp (colorValues [0], colorValues [1], animationCurve_.Evaluate (t));

					yield return 0;
				}

				boolValues [0] = false;
			}
			yield return 0;
		}

		void ComputeColor ()
		{
			if ( ! doIT)
			{
				return;
			}


			exAnimationCurves_Idle_Logic.exAnimationCurves_Idle.exAnimationCurves.StartCoroutine (RunAnimationCurve_color ());

		}
	}
}
